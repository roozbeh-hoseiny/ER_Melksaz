using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.GenerationSteps.MessageGenerationSteps;

internal sealed class MessageApiRequestMapperGeneratorStep : IProtoMessageGenerationStep
{
    private readonly IProtoTypeClassifierResolver _protoTypeClassifierResolver;
    private readonly IAssignmentGeneratorResolver _assignmentGeneratorResolver;
    private readonly IMessageNameResolver _messageNameResolver;

    public int Order => 3;

    public MessageApiRequestMapperGeneratorStep(
        IProtoTypeClassifierResolver protoTypeClassifierResolver,
        IAssignmentGeneratorResolver assignmentGeneratorResolver,
        IMessageNameResolver messageNameResolver)
    {
        this._protoTypeClassifierResolver = protoTypeClassifierResolver;
        this._assignmentGeneratorResolver = assignmentGeneratorResolver;
        this._messageNameResolver = messageNameResolver;
    }

    public void Execute(ProtoMessage message, GenerationContext context)
    {
        if (!message.CanCreateClass)
            return;

        if (
            message.Annotations.Has<ApiResponseMessageType>()
            || message.Annotations.Has<ApiReplyMessageType>())
            return;

        var messageDocumentKey = message.GetDocumentKey();

        var builder = context.GetBuilder<CSharpClassBuilder>(messageDocumentKey);

        var apiType = this._messageNameResolver.GetOrCreate(message);

        var grpcType = message.FullName;

        var assignments = new List<ExpressionSyntax>();

        foreach (var property in message.Properties)
        {
            var targetPropertyAnnotation = property.Annotations.Get<CSharpPropertyAnnotation>();
            var sourcePropertyAnnotation = property.Annotations.Get<PropertyNameAnnotation>();
            var typeKind = this._protoTypeClassifierResolver.Classify(property);

            var assignmentContext =
                new AssignmentGenerationContext
                {
                    Message = message,
                    Property = property,
                    SourceExpression = SyntaxFactory.IdentifierName("src"),
                    TargetExpression =
                        SyntaxFactory.IdentifierName(
                            property.Annotations
                                .Get<CSharpPropertyAnnotation>()?.Name
                                ?? property.ProtoName),
                    SourcePropertyName =
                        sourcePropertyAnnotation?.Name
                        ?? property.ProtoName,
                    TargetPropertyName =
                        targetPropertyAnnotation?.Name
                        ?? property.ProtoName,
                    TypeKind = typeKind,
                };

            var generator = this._assignmentGeneratorResolver.Resolve(assignmentContext.TypeKind);

            ExpressionSyntax value =
                property.IsRepeated
                    ? generator.GenerateRepeatedValue(assignmentContext)
                    : generator.GenerateValue(assignmentContext);

            assignments.Add(
                SyntaxFactory.AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression,
                    assignmentContext.TargetExpression,
                    value));
        }

        var objectCreation =
            SyntaxFactory.ObjectCreationExpression(
                    SyntaxFactory.ParseTypeName(
                        grpcType))
                .WithInitializer(
                    SyntaxFactory.InitializerExpression(
                        SyntaxKind.ObjectInitializerExpression,
                        SyntaxFactory.SeparatedList(assignments)));

        var method =
            SyntaxFactory.MethodDeclaration(
                    SyntaxFactory.ParseTypeName(
                        $"{grpcType}?"),
                    "MapToGrpcRequest")
                .AddModifiers(
                    SyntaxFactory.Token(
                        SyntaxKind.PublicKeyword),
                    SyntaxFactory.Token(
                        SyntaxKind.StaticKeyword))
                .AddParameterListParameters(
                    SyntaxFactory.Parameter(
                            SyntaxFactory.Identifier("src"))
                        .WithType(
                            SyntaxFactory.ParseTypeName(
                                $"{apiType.ClassName}?")))
                .WithBody(
                    SyntaxFactory.Block(

                        SyntaxFactory.IfStatement(
                            SyntaxFactory.IsPatternExpression(
                                SyntaxFactory.IdentifierName("src"),
                                SyntaxFactory.ConstantPattern(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.NullLiteralExpression))),
                            SyntaxFactory.ReturnStatement(
                                SyntaxFactory.LiteralExpression(
                                    SyntaxKind.NullLiteralExpression))),

                        SyntaxFactory.ReturnStatement(
                            objectCreation)));

        builder.UpdateClass(cls => cls.AddMembers(method));
    }
}