using System;

[Serializable]
public class Tuple<T1>
{
    public T1 _1 { get; protected set; }

    public static Tuple<T1> Of(T1 t1)
    {
        return new Tuple<T1>(t1);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        var objAsTuple = obj as Tuple<T1>;
        if (objAsTuple == null)
        {
            return false;
        }
        return _1.Equals(objAsTuple._1);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    protected Tuple(T1 t1)
    {
        _1 = t1;
    }
}

[Serializable]
public class Tuple<T1, T2>
{
    public T1 _1 { get; protected set; }
    public T2 _2 { get; protected set; }

    public static Tuple<T1, T2> Of(T1 t1, T2 t2)
    {
        return new Tuple<T1, T2>(t1, t2);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        var objAsTuple = obj as Tuple<T1, T2>;
        if (objAsTuple == null)
        {
            return false;
        }
        if (!(_1.Equals(objAsTuple._1)))
        {
            return false;
        }
        return _2.Equals(objAsTuple._2);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    protected Tuple(T1 t1, T2 t2)
    {
        _1 = t1;
        _2 = t2;
    }
}

[Serializable]
public class Tuple<T1, T2, T3>
{
    public T1 _1 { get; protected set; }
    public T2 _2 { get; protected set; }
    public T3 _3 { get; protected set; }

    public static Tuple<T1, T2, T3> Of(T1 t1, T2 t2, T3 t3)
    {
        return new Tuple<T1, T2, T3>(t1, t2, t3);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        var objAsTuple = obj as Tuple<T1, T2, T3>;
        if (objAsTuple == null)
        {
            return false;
        }
        if (!(_1.Equals(objAsTuple._1)))
        {
            return false;
        }
        if (!(_2.Equals(objAsTuple._2)))
        {
            return false;
        }
        return _3.Equals(objAsTuple._3);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    protected Tuple(T1 t1, T2 t2, T3 t3)
    {
        _1 = t1;
        _2 = t2;
        _3 = t3;
    }
}

[Serializable]
public class Tuple<T1, T2, T3, T4>
{
    public T1 _1 { get; protected set; }
    public T2 _2 { get; protected set; }
    public T3 _3 { get; protected set; }
    public T4 _4 { get; protected set; }

    public static Tuple<T1, T2, T3, T4> Of(T1 t1, T2 t2, T3 t3, T4 t4)
    {
        return new Tuple<T1, T2, T3, T4>(t1, t2, t3, t4);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        var objAsTuple = obj as Tuple<T1, T2, T3, T4>;
        if (objAsTuple == null)
        {
            return false;
        }
        if (!(_1.Equals(objAsTuple._1)))
        {
            return false;
        }
        if (!(_2.Equals(objAsTuple._2)))
        {
            return false;
        }
        if (!(_3.Equals(objAsTuple._3)))
        {
            return false;
        }
        return _4.Equals(objAsTuple._4);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    protected Tuple(T1 t1, T2 t2, T3 t3, T4 t4)
    {
        _1 = t1;
        _2 = t2;
        _3 = t3;
        _4 = t4;
    }
}

[Serializable]
public class Tuple<T1, T2, T3, T4, T5>
{
    public T1 _1 { get; protected set; }
    public T2 _2 { get; protected set; }
    public T3 _3 { get; protected set; }
    public T4 _4 { get; protected set; }
    public T5 _5 { get; protected set; }

    public static Tuple<T1, T2, T3, T4, T5> Of(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
    {
        return new Tuple<T1, T2, T3, T4, T5>(t1, t2, t3, t4, t5);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        var objAsTuple = obj as Tuple<T1, T2, T3, T4, T5>;
        if (objAsTuple == null)
        {
            return false;
        }
        if (!(_1.Equals(objAsTuple._1)))
        {
            return false;
        }
        if (!(_2.Equals(objAsTuple._2)))
        {
            return false;
        }
        if (!(_3.Equals(objAsTuple._3)))
        {
            return false;
        }
        if (!(_4.Equals(objAsTuple._4)))
        {
            return false;
        }
        return _5.Equals(objAsTuple._5);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    protected Tuple(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
    {
        _1 = t1;
        _2 = t2;
        _3 = t3;
        _4 = t4;
        _5 = t5;
    }
}

[Serializable]
public class Tuple<T1, T2, T3, T4, T5, T6>
{
    public T1 _1 { get; protected set; }
    public T2 _2 { get; protected set; }
    public T3 _3 { get; protected set; }
    public T4 _4 { get; protected set; }
    public T5 _5 { get; protected set; }
    public T6 _6 { get; protected set; }

    public static Tuple<T1, T2, T3, T4, T5, T6> Of(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
    {
        return new Tuple<T1, T2, T3, T4, T5, T6>(t1, t2, t3, t4, t5, t6);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        var objAsTuple = obj as Tuple<T1, T2, T3, T4, T5, T6>;
        if (objAsTuple == null)
        {
            return false;
        }
        if (!(_1.Equals(objAsTuple._1)))
        {
            return false;
        }
        if (!(_2.Equals(objAsTuple._2)))
        {
            return false;
        }
        if (!(_3.Equals(objAsTuple._3)))
        {
            return false;
        }
        if (!(_4.Equals(objAsTuple._4)))
        {
            return false;
        }
        if (!(_5.Equals(objAsTuple._5)))
        {
            return false;
        }
        return _6.Equals(objAsTuple._6);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    protected Tuple(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
    {
        _1 = t1;
        _2 = t2;
        _3 = t3;
        _4 = t4;
        _5 = t5;
        _6 = t6;
    }
}

[Serializable]
public class Tuple<T1, T2, T3, T4, T5, T6, T7>
{
    public T1 _1 { get; protected set; }
    public T2 _2 { get; protected set; }
    public T3 _3 { get; protected set; }
    public T4 _4 { get; protected set; }
    public T5 _5 { get; protected set; }
    public T6 _6 { get; protected set; }
    public T7 _7 { get; protected set; }

    public static Tuple<T1, T2, T3, T4, T5, T6, T7> Of(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
    {
        return new Tuple<T1, T2, T3, T4, T5, T6, T7>(t1, t2, t3, t4, t5, t6, t7);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        var objAsTuple = obj as Tuple<T1, T2, T3, T4, T5, T6, T7>;
        if (objAsTuple == null)
        {
            return false;
        }
        if (!(_1.Equals(objAsTuple._1)))
        {
            return false;
        }
        if (!(_2.Equals(objAsTuple._2)))
        {
            return false;
        }
        if (!(_3.Equals(objAsTuple._3)))
        {
            return false;
        }
        if (!(_4.Equals(objAsTuple._4)))
        {
            return false;
        }
        if (!(_5.Equals(objAsTuple._5)))
        {
            return false;
        }
        if (!(_6.Equals(objAsTuple._6)))
        {
            return false;
        }
        return _7.Equals(objAsTuple._7);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    protected Tuple(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
    {
        _1 = t1;
        _2 = t2;
        _3 = t3;
        _4 = t4;
        _5 = t5;
        _6 = t6;
        _7 = t7;
    }
}

[Serializable]
public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8>
{
    public T1 _1 { get; protected set; }
    public T2 _2 { get; protected set; }
    public T3 _3 { get; protected set; }
    public T4 _4 { get; protected set; }
    public T5 _5 { get; protected set; }
    public T6 _6 { get; protected set; }
    public T7 _7 { get; protected set; }
    public T8 _8 { get; protected set; }

    public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8> Of(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8)
    {
        return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8>(t1, t2, t3, t4, t5, t6, t7, t8);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        var objAsTuple = obj as Tuple<T1, T2, T3, T4, T5, T6, T7, T8>;
        if (objAsTuple == null)
        {
            return false;
        }
        if (!(_1.Equals(objAsTuple._1)))
        {
            return false;
        }
        if (!(_2.Equals(objAsTuple._2)))
        {
            return false;
        }
        if (!(_3.Equals(objAsTuple._3)))
        {
            return false;
        }
        if (!(_4.Equals(objAsTuple._4)))
        {
            return false;
        }
        if (!(_5.Equals(objAsTuple._5)))
        {
            return false;
        }
        if (!(_6.Equals(objAsTuple._6)))
        {
            return false;
        }
        if (!(_7.Equals(objAsTuple._7)))
        {
            return false;
        }
        return _8.Equals(objAsTuple._8);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    protected Tuple(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8)
    {
        _1 = t1;
        _2 = t2;
        _3 = t3;
        _4 = t4;
        _5 = t5;
        _6 = t6;
        _7 = t7;
        _8 = t8;
    }
}
