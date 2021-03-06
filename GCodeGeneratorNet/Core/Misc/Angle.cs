﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeGeneratorNet.Core.Misc
{
    public class Angle
    {
        double angle = 0;

        public Angle(Vector2 v)
        {
            var xv = new Vector2(1, 0);
            v.Normalize();
            var cos = Vector2.Dot(xv, v);
            angle = Math.Acos(cos);
            if (v.Y < 0)
            {
                angle = Math.PI*2 - angle;
            }
        }

        public Angle(double _angle)
        {
            angle = _angle;
        }

        public Vector2 HorizontalVector
        {
            get
            {
                return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            }
        }

        public bool EqualEps(Angle other, double eps)
        {
            return Math.Abs(this - other) < eps;
        }

        public void Increment(double inc)
        {
            angle += inc;
            if (angle > Math.PI * 2)
                angle -= Math.PI * 2;
            else if (angle < 0)
                angle += Math.PI * 2;
        }
        public void Decrement(double dec)
        {
            Increment(-dec);
        }
        public override string ToString()
        {
            return angle.ToString();
        }
        public override int GetHashCode()
        {
            return angle.GetHashCode();
        }
        public static implicit operator double(Angle angleObj)
        {
            return angleObj.angle;
        }
        public static implicit operator Angle(double _angle)
        {
            return new Angle(_angle);
        }
        public static Angle operator +(Angle lhs, Angle rhs)
        {
            Angle angle = new Angle(lhs.angle);
            angle.Increment(rhs.angle);
            return angle;
        }
        public static Angle operator -(Angle lhs, Angle rhs)
        {
            Angle angle = new Angle(lhs.angle);
            angle.Decrement(rhs.angle);
            return angle;
        }
        public static bool operator <(Angle lhs, Angle rhs)
        {
            if (lhs.angle < rhs.angle)
                return true;
            return false;
        }
        public static bool operator <=(Angle lhs, Angle rhs)
        {
            if (lhs.angle <= rhs.angle)
                return true;
            return false;
        }
        public static bool operator >(Angle lhs, Angle rhs)
        {
            if (lhs.angle > rhs.angle)
                return true;
            return false;
        }
        public static bool operator >=(Angle lhs, Angle rhs)
        {
            if (lhs.angle >= rhs.angle)
                return true;
            return false;
        }

        public static IEnumerable<Angle> Angles(int count, double startAngle = 0, double stopAngle = 0)
        {
            double delta;
            if(startAngle == stopAngle)
            {
                delta = Math.PI * 2 / count;
            }
            else
            {
                delta = (stopAngle - startAngle) / count;
            }
            Angle a = startAngle;
            for(int i = 0; i < count; i++)
            {
                yield return a;
                a += delta;
            }
        }

        public static Angle Degrees(double degrees)
        {
            return (degrees / 180.0) * Math.PI;
        }
    }
}
