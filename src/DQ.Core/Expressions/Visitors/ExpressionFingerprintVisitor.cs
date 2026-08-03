using System.Linq.Expressions;
using System.Text;

namespace DQ.Core.Expressions.Visitors;

/// <summary>
/// Generates a normalized textual representation of an expression tree.
/// </summary>
/// <remarks>
/// The generated fingerprint ignores parameter instance identity and
/// creates a stable representation for structurally equivalent expressions.
///
/// This representation is used as the basis for expression caching.
/// </remarks>
internal sealed class ExpressionFingerprintVisitor : ExpressionVisitor
{

    private readonly StringBuilder _builder = new();
    private readonly Dictionary<ParameterExpression, string> _parameters = [];
    private int _parameterIndex;



    /// <summary>
    /// Generates the fingerprint value.
    /// </summary>
    public string GetFingerprint()
    {
        return this._builder.ToString();
    }

    /// <inheritdoc />
    protected override Expression VisitLambda<T>(Expression<T> node)
    {
        this._builder.Append("Lambda(");

        foreach (var parameter in node.Parameters)
        {
            this.Visit(parameter);
        }

        this._builder.Append(")");

        this.Visit(node.Body);

        return node;
    }

    /// <inheritdoc />
    protected override Expression VisitParameter(ParameterExpression node)
    {
        if (!this._parameters.TryGetValue(node, out var name))
        {
            name = $"p{this._parameterIndex++}";
            this._parameters[node] = name;
        }

        this._builder.Append("Parameter:");
        this._builder.Append(name);
        this._builder.Append(";");

        return node;
    }

    /// <inheritdoc />
    protected override Expression VisitMember(MemberExpression node)
    {
        this._builder.Append("Member:");
        this._builder.Append(node.Member.DeclaringType?.FullName);
        this._builder.Append(".");
        this._builder.Append(node.Member.Name);
        this._builder.Append(";");

        this.Visit(node.Expression);

        return node;
    }

    /// <inheritdoc />
    protected override Expression VisitConstant(ConstantExpression node)
    {
        this._builder.Append("Constant:");

        if (node.Value is null)
        {
            this._builder.Append("null");
        }
        else
        {
            this._builder.Append(node.Type.FullName);
            this._builder.Append(":");
            this._builder.Append(node.Value);
        }

        this._builder.Append(";");

        return node;
    }

    /// <inheritdoc />
    protected override Expression VisitBinary(BinaryExpression node)
    {
        this._builder.Append("Binary:");
        this._builder.Append(node.NodeType);
        this._builder.Append(";");

        this.Visit(node.Left);
        this.Visit(node.Right);

        return node;
    }

    /// <inheritdoc />
    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        this._builder.Append("Method:");
        this._builder.Append(node.Method.DeclaringType?.FullName);
        this._builder.Append(".");
        this._builder.Append(node.Method.Name);
        this._builder.Append(";");

        foreach (var argument in node.Arguments)
        {
            this.Visit(argument);
        }

        return node;
    }
}