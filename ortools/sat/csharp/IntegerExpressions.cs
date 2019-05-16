// Copyright 2010-2018 Google LLC
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Google.OrTools.Sat
{
  using System;
  using System.Collections.Generic;
  using Google.OrTools.Util;

  // Helpers.

  // IntVar[] helper class.
  public static class IntVarArrayHelper
  {
    public static LinearExpr Sum(this IntVar[] vars)
    {
      return LinearExpr.Sum(vars);
    }
    public static LinearExpr ScalProd(this IntVar[] vars, int[] coeffs)
    {
      return LinearExpr.ScalProd(vars, coeffs);
    }
    public static LinearExpr ScalProd(this IntVar[] vars, long[] coeffs)
    {
      return LinearExpr.ScalProd(vars, coeffs);
    }
  }

  public interface ILiteral
  {
    ILiteral Not();
    int GetIndex();
  }

  // Holds an linear expression.
  public class LinearExpr
  {

    public static LinearExpr Sum(IEnumerable<IntVar> vars)
    {
      return new SumArray(vars);
    }

    public static LinearExpr ScalProd(IEnumerable<IntVar> vars, IEnumerable<int> coeffs)
    {
      return new SumArray(vars, coeffs);
    }
    public static LinearExpr ScalProd(IEnumerable<IntVar> vars, IEnumerable<long> coeffs)
    {
      return new SumArray(vars, coeffs);
    }

    public int Index
    {
      get { return GetIndex(); }
    }

    public virtual int GetIndex()
    {
      throw new NotImplementedException();
    }

    public virtual string ShortString()
    {
      return ToString();
    }

    public static LinearExpr operator +(LinearExpr a, LinearExpr b)
    {
      return new SumArray(a, b);
    }

    public static LinearExpr operator +(LinearExpr a, long v)
    {
      return new SumArray(a, v);
    }

    public static LinearExpr operator +(long v, LinearExpr a)
    {
      return new SumArray(a, v);
    }

    public static LinearExpr operator -(LinearExpr a, LinearExpr b)
    {
      return new SumArray(a, Prod(b, -1));
    }

    public static LinearExpr operator -(LinearExpr a, long v)
    {
      return new SumArray(a, -v);
    }

    public static LinearExpr operator -(long v, LinearExpr a)
    {
      return new SumArray(Prod(a, -1), v);
    }

    public static LinearExpr operator *(LinearExpr a, long v)
    {
      return Prod(a, v);
    }

    public static LinearExpr operator *(long v, LinearExpr a)
    {
      return Prod(a, v);
    }

    public static LinearExpr operator -(LinearExpr a)
    {
      return Prod(a, -1);
    }

    public static BoundedLinearExpression operator ==(LinearExpr a, LinearExpr b)
    {
      return new BoundedLinearExpression(a, b, true);
    }

    public static BoundedLinearExpression operator !=(LinearExpr a, LinearExpr b)
    {
      return new BoundedLinearExpression(a, b, false);
    }

    public static BoundedLinearExpression operator ==(LinearExpr a, long v)
    {
      return new BoundedLinearExpression(a, v, true);
    }

    public static BoundedLinearExpression operator !=(LinearExpr a, long v)
    {
      return new BoundedLinearExpression(a, v, false);
    }

    public static BoundedLinearExpression operator >=(LinearExpr a, long v)
    {
      return new BoundedLinearExpression(v, a, Int64.MaxValue);
    }

    public static BoundedLinearExpression operator >=(long v, LinearExpr a)
    {
      return a <= v;
    }

    public static BoundedLinearExpression operator >(LinearExpr a, long v)
    {
      return new BoundedLinearExpression(v + 1, a, Int64.MaxValue);
    }

    public static BoundedLinearExpression operator >(long v, LinearExpr a)
    {
      return a < v;
    }

    public static BoundedLinearExpression operator <=(LinearExpr a, long v)
    {
      return new BoundedLinearExpression(Int64.MinValue, a, v);
    }

    public static BoundedLinearExpression operator <=(long v, LinearExpr a)
    {
      return a >= v;
    }

    public static BoundedLinearExpression operator <(LinearExpr a, long v)
    {
      return new BoundedLinearExpression(Int64.MinValue, a, v - 1);
    }

    public static BoundedLinearExpression operator <(long v, LinearExpr a)
    {
      return a > v;
    }

    public static BoundedLinearExpression operator >=(LinearExpr a, LinearExpr b)
    {
      return new BoundedLinearExpression(0, a - b, Int64.MaxValue);
    }

    public static BoundedLinearExpression operator >(LinearExpr a, LinearExpr b)
    {
      return new BoundedLinearExpression(1, a - b, Int64.MaxValue);
    }

    public static BoundedLinearExpression operator <=(LinearExpr a, LinearExpr b)
    {
      return new BoundedLinearExpression(Int64.MinValue, a - b, 0);
    }

    public static BoundedLinearExpression operator <(LinearExpr a, LinearExpr b)
    {
      return new BoundedLinearExpression(Int64.MinValue, a - b, -1);
    }

    public static LinearExpr Prod(LinearExpr e, long v)
    {
      if (v == 1)
      {
        return e;
      }
      else if (e is ProductCst)
      {
        ProductCst p = (ProductCst)e;
        return new ProductCst(p.Expr, p.Coeff * v);
      }
      else
      {
        return new ProductCst(e, v);
      }
    }

    public static long GetVarValueMap(LinearExpr e,
                                      long initial_coeff,
                                      Dictionary<IntVar, long> dict)
    {
      List<LinearExpr> exprs = new List<LinearExpr>();
      List<long> coeffs = new List<long>();
      if ((Object)e != null) {
        exprs.Add(e);
        coeffs.Add(initial_coeff);
      }
      long constant = 0;

      while (exprs.Count > 0)
      {
        LinearExpr expr = exprs[0];
        exprs.RemoveAt(0);
        long coeff = coeffs[0];
        coeffs.RemoveAt(0);
        if (coeff == 0 || (Object)expr == null) continue;

        if (expr is ProductCst)
        {
          ProductCst p = (ProductCst)expr;
          if (p.Coeff != 0)
          {
            exprs.Add(p.Expr);
            coeffs.Add(p.Coeff * coeff);
          }
        }
        else if (expr is SumArray)
        {
          SumArray a = (SumArray)expr;
          constant += coeff * a.Constant;
          foreach (LinearExpr sub in a.Expressions)
          {
            exprs.Add(sub);
          }
          foreach (long c in a.Coefficients)
          {
            coeffs.Add(coeff * c);
          }
        }
        else if (expr is IntVar)
        {
          IntVar i = (IntVar)expr;
          if (dict.ContainsKey(i))
          {
            dict[i] += coeff;
          }
          else
          {
            dict.Add(i, coeff);
          }

        }
        else if (expr is NotBooleanVariable)
        {
          throw new ArgumentException(
              "Cannot interpret a literal in an integer expression.");
        }
        else
        {
          throw new ArgumentException("Cannot interpret '" + expr.ToString() +
                                      "' in an integer expression");
        }
      }
      return constant;
    }
  }

  public class ProductCst : LinearExpr
  {
    public ProductCst(LinearExpr e, long v)
    {
      expr_ = e;
      coeff_ = v;
    }

    public LinearExpr Expr
    {
      get { return expr_; }
    }

    public long Coeff
    {
      get { return coeff_; }
    }

    private LinearExpr expr_;
    private long coeff_;

  }

  public class SumArray : LinearExpr
  {
    public SumArray(LinearExpr a, LinearExpr b)
    {
      Init(2);
      AddExpr(a, 0);
      AddExpr(b, 1);
      constant_ = 0L;
    }

    public SumArray(LinearExpr a, long b)
    {
      Init(1);
      AddExpr(a, 0);
      constant_ = b;
    }

    public SumArray(IEnumerable<LinearExpr> exprs)
    {
      int count = FindLength(exprs);
      Init(count);
      int index = 0;
      foreach (LinearExpr e in exprs)
      {
        AddExpr(e, index);
        index++;
      }
      constant_ = 0L;
    }

    public SumArray(IEnumerable<IntVar> vars)
    {
      int count = FindLength(vars);
      Init(count);
      int index = 0;
      foreach (IntVar v in vars)
      {
        AddExpr(v, index);
        index++;
      }
      constant_ = 0L;
    }
    
    public SumArray(IEnumerable<IntVar> vars, IEnumerable<long> coeffs)
    {
      int count = FindLength(vars);
      Init(count);
      // Fill the coefficients;
      int index = 0;
      foreach (int c in coeffs)
      {
        coefficients_[index] = c;
        index++;
      }
      // Add terms, reading the filled coefficients.
      index = 0;
      foreach (LinearExpr v in vars)
      {
         AddTerm(v, coefficients_[index], index);
         index++;
      }
      constant_ = 0L;
    }
    
    public SumArray(IEnumerable<IntVar> vars, IEnumerable<int> coeffs)
    {
      int count = FindLength(vars);
      Init(count);
      // Fill the coefficients;
      int index = 0;
      foreach (int c in coeffs)
      {
        coefficients_[index] = c;
        index++;
      }
      // Add terms, reading the filled coefficients.
      index = 0;
      foreach (LinearExpr v in vars)
      {
         AddTerm(v, coefficients_[index], index);
         index++;
      }
      constant_ = 0L;
    }

    public void AddExpr(LinearExpr expr, int index) {
      if (expr is ProductCst)
      {
        ProductCst p = (ProductCst)expr;
        expressions_[index] = p.Expr;
        coefficients_[index] = p.Coeff;
      }
      else
      {
        expressions_[index] = expr;
        coefficients_[index] = 1;
      }
    }

    public void AddTerm(LinearExpr expr, long coeff, int index) {
      if (expr is ProductCst)
      {
        ProductCst p = (ProductCst)expr;
        expressions_[index] = p.Expr;
        coefficients_[index] = p.Coeff * coeff;
      }
      else
      {
        expressions_[index] = expr;
        coefficients_[index] = coeff;
      }
    }    

    public LinearExpr[] Expressions
    {
      get { return expressions_; }
    }

    public long[] Coefficients
    {
      get { return coefficients_; }
    }
    
    public long Constant
    {
      get { return constant_; }
    }

    void Init(int size) {
      expressions_ = new LinearExpr[size];
      coefficients_ = new long[size];
    }

    int FindLength(IEnumerable<LinearExpr> exprs) {
      int count = 0;
      foreach (LinearExpr e in exprs) {
        count++;
      }
      return count;
    }

    int FindLength(IEnumerable<IntVar> vars) {
      int count = 0;
      foreach (IntVar v in vars) {
        count++;
      }
      return count;
    }

    public override string ShortString()
    {
      return String.Format("({0})", ToString());
    }

    public override string ToString()
    {
      string result = "";
      for (int i = 0; i < expressions_.Length; ++i)
      {
        LinearExpr expr = expressions_[i];
        if ((Object)expr == null) continue;
        long coeff = coefficients_[i];
        if (i != 0)
        {
          if (coeff < 0)
          {
            result += String.Format(" - ");
            coeff = -coeff;
          }
          else
          {
            result += String.Format(" + ");
          }
        }

        if (coeff == 1)
        {
          result += expr.ShortString();
        }
        else if (coeff == -1)
        {
          result += String.Format("-{0}", expr);
        }
        else
        {
          result += String.Format("{0}*{1}", coeff, expr);
        }
      }
      return result;
    }

    private LinearExpr[] expressions_;
    private long[] coefficients_;
    private long constant_;

  }

  public class IntVar : LinearExpr, ILiteral
  {
    public IntVar(CpModelProto model, Domain domain, string name)
    {
      model_ = model;
      index_ = model.Variables.Count;
      var_ = new IntegerVariableProto();
      var_.Name = name;
      var_.Domain.Add(domain.FlattenedIntervals());
      model.Variables.Add(var_);
      negation_ = null;
    }

    public int Index
    {
      get { return index_; }
    }

    public override int GetIndex()
    {
      return index_;
    }

    public IntegerVariableProto Proto
    {
      get { return var_; }
      set { var_ = value; }
    }

    public override string ToString()
    {
      return var_.ToString();
    }

    public override string ShortString()
    {
      if (var_.Name != null)
      {
        return var_.Name;
      }
      else
      {
        return var_.ToString();
      }
    }

    public string Name()
    {
      return var_.Name;
    }

    public ILiteral Not()
    {
      foreach (long b in var_.Domain)
      {
        if (b < 0 || b > 1)
        {
          throw new ArgumentException(
              "Cannot call Not() on a non boolean variable");
        }
      }
      if (negation_ == null)
      {
        negation_ = new NotBooleanVariable(this);
      }
      return negation_;
    }


    private CpModelProto model_;
    private int index_;
    private List<long> bounds_;
    private IntegerVariableProto var_;
    private NotBooleanVariable negation_;
  }

  public class NotBooleanVariable : LinearExpr, ILiteral
  {
    public NotBooleanVariable(IntVar boolvar)
    {
      boolvar_ = boolvar;
    }

    public override int GetIndex()
    {
      return -boolvar_.Index - 1;
    }

    public ILiteral Not()
    {
      return boolvar_;
    }

    public override string ShortString()
    {
      return String.Format("Not({0})", boolvar_.ShortString());
    }

    private IntVar boolvar_;
  }

  public class BoundedLinearExpression
  {
    public enum Type
    {
      BoundExpression,
      VarEqVar,
      VarDiffVar,
      VarEqCst,
      VarDiffCst,
    }

    public BoundedLinearExpression(long lb, LinearExpr expr, long ub)
    {
      left_ = expr;
      right_ = null;
      lb_ = lb;
      ub_ = ub;
      type_ = Type.BoundExpression;
    }

    public BoundedLinearExpression(LinearExpr left, LinearExpr right,
                                  bool equality)
    {
      left_ = left;
      right_ = right;
      lb_ = 0;
      ub_ = 0;
      type_ = equality ? Type.VarEqVar : Type.VarDiffVar;
    }

    public BoundedLinearExpression(LinearExpr left, long v, bool equality)
    {
      left_ = left;
      right_ = null;
      lb_ = v;
      ub_ = 0;
      type_ = equality ? Type.VarEqCst : Type.VarDiffCst;
    }

    bool IsTrue()
    {
      if (type_ == Type.VarEqVar)
      {
        return (object)left_ == (object)right_;
      }
      else if (type_ == Type.VarDiffVar)
      {
        return (object)left_ != (object)right_;
      }
      return false;
    }

    public static bool operator true(BoundedLinearExpression bie)
    {
      return bie.IsTrue();
    }

    public static bool operator false(BoundedLinearExpression bie)
    {
      return !bie.IsTrue();
    }

    public override string ToString()
    {
      switch (type_)
      {
        case Type.BoundExpression:
          return String.Format("{0} <= {1} <= {2}", lb_, left_, ub_);
        case Type.VarEqVar:
          return String.Format("{0} == {1}", left_, right_);
        case Type.VarDiffVar:
          return String.Format("{0} != {1}", left_, right_);
        case Type.VarEqCst:
          return String.Format("{0} == {1}", left_, lb_);
        case Type.VarDiffCst:
          return String.Format("{0} != {1}", left_, lb_);
        default:
          throw new ArgumentException("Wrong mode in BoundedLinearExpression.");
      }
    }

    public static BoundedLinearExpression operator <=(BoundedLinearExpression a,
                                                     long v)
    {
      if (a.CtType != Type.BoundExpression || a.Ub != Int64.MaxValue)
      {
        throw new ArgumentException(
            "Operator <= not supported for this BoundedLinearExpression");
      }
      return new BoundedLinearExpression(a.Lb, a.Left, v);
    }

    public static BoundedLinearExpression operator <(BoundedLinearExpression a,
                                                    long v)
    {
      if (a.CtType != Type.BoundExpression || a.Ub != Int64.MaxValue)
      {
        throw new ArgumentException(
            "Operator < not supported for this BoundedLinearExpression");
      }
      return new BoundedLinearExpression(a.Lb, a.Left, v - 1);
    }

    public static BoundedLinearExpression operator >=(BoundedLinearExpression a,
                                                     long v)
    {
      if (a.CtType != Type.BoundExpression || a.Lb != Int64.MinValue)
      {
        throw new ArgumentException(
            "Operator >= not supported for this BoundedLinearExpression");
      }
      return new BoundedLinearExpression(v, a.Left, a.Ub);
    }

    public static BoundedLinearExpression operator >(BoundedLinearExpression a,
                                                    long v)
    {
      if (a.CtType != Type.BoundExpression || a.Lb != Int64.MinValue)
      {
        throw new ArgumentException(
            "Operator < not supported for this BoundedLinearExpression");
      }
      return new BoundedLinearExpression(v + 1, a.Left, a.Ub);
    }

    public LinearExpr Left
    {
      get { return left_; }
    }

    public LinearExpr Right
    {
      get { return right_; }
    }

    public long Lb
    {
      get { return lb_; }
    }

    public long Ub
    {
      get { return ub_; }
    }

    public Type CtType
    {
      get { return type_; }
    }

    private LinearExpr left_;
    private LinearExpr right_;
    private long lb_;
    private long ub_;
    private Type type_;
  }

}  // namespace Google.OrTools.Sat
