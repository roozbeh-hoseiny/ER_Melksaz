using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProtoWeaver.Generation.Contracts;
using ProtoWeaver.Generation.CSharpGenerator.AnnotationProcessors.MessageAnnotationProcessors;
using ProtoWeaver.Generation.CSharpGenerator.Annotations;
using ProtoWeaver.Generation.Mapping;
using ProtoWeaver.Models;

namespace ProtoWeaver.Generation.CSharpGenerator.GenerationSteps.MessageGenerationSteps;

internal sealed class MessageApiRequestMapperGeneratorStep : IProtoMessageGenerationStep
{
    private readonly IAssignmentGeneratorResolver _assignmentGeneratorResolver;
    private readonly IMessageNameResolver _messageNameResolver;

    public int Order => 3;

    public MessageApiRequestMapperGeneratorStep(
        IAssignmentGeneratorResolver assignmentGeneratorResolver,
        IMessageNameResolver messageNameResolver)
    {
        this._assignmentGeneratorResolver = assignmentGeneratorResolver;
        this._messageNameResolver = messageNameResolver;
    }

    public void Execute(ProtoMessage message, GenerationContext context)
    {
        if (!message.CanCreateClass)
            return;

        var messageType = message.Annotations.GetDerived<IMessageTypeBase>();

        if (messageType is not ApiRequestMessageType)
            return;

        var messageDocumentKey = message.GetDocumentKey();

        var builder = context.GetBuilder<CSharpClassBuilder>(messageDocumentKey);

        var apiType = this._messageNameResolver.GetOrCreate(message);

        var grpcType = message.FullName;

        var assignments = new List<ExpressionSyntax>();

        foreach (var property in message.Properties)
        {
            var propertyAnnotation =
                property.Annotations.Get<CSharpPropertyAnnotation>();

            var assignmentContext =
                new AssignmentGenerationContext
                {
                    Message = message,
                    Property = property,
                    SourceExpression =
                        SyntaxFactory.IdentifierName("src"),

                    TargetPropertyName =
                        propertyAnnotation?.Name
                        ?? property.ProtoName
                };

            var generator =
                this._assignmentGeneratorResolver.Resolve(
                    assignmentContext);

            assignments.Add(
                generator.Generate(
                    assignmentContext));
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