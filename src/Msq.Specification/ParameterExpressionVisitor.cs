using System.Linq.Expressions;

namespace Msq.Specification
{
    internal class ParameterExpressionVisitor : ExpressionVisitor
    {
        private ParameterExpression parameterExpression;

        internal ParameterExpressionVisitor(ParameterExpression parameterExpressionNode)
        {
            parameterExpression = parameterExpressionNode;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.Type != parameterExpression.Type)
                parameterExpression = Expression.Parameter(node.Type);

            return base.VisitParameter(parameterExpression);
        }
    }
}
