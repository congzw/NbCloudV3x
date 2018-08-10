using System;
using System.Collections.Generic;
using System.Reflection;

namespace ZQNB.Common.Data.Model
{
    public interface INbEntity<TPk>
    {
        /// <summary>
        /// 主键
        /// </summary>
        TPk Id { get; set; }
        //Infoset Infoset { get; set; }
    }

    public abstract class NbEntityBase<T, TPk> : INbEntity<TPk> where T : class, INbEntity<TPk>
    {
        public virtual TPk Id { get; set; }

        private readonly TPk _defaultIdValue = default(TPk);
        private int? _oldHashCode;

        public override int GetHashCode()
        {
            // once we have a hashcode we'll never change it
            if (_oldHashCode.HasValue)
                return _oldHashCode.Value;
            // when this instance is new we use the base hash code
            // and remember it, so an instance can NEVER change its
            // hash code.
            var thisIsNew = Equals(Id, _defaultIdValue);
            if (thisIsNew)
            {
                _oldHashCode = base.GetHashCode();
                return _oldHashCode.Value;
            }
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as T;
            if (other == null) return false;

            var thisIsNew = Equals(Id, _defaultIdValue);
            var otherIsNew = Equals(other.Id, _defaultIdValue);

            if (thisIsNew && otherIsNew)
                return ReferenceEquals(this, other);

            return Id.Equals(other.Id);
        }

        public static bool operator ==(NbEntityBase<T, TPk> lhs, NbEntityBase<T, TPk> rhs)
        {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(NbEntityBase<T, TPk> lhs, NbEntityBase<T, TPk> rhs)
        {
            return !Equals(lhs, rhs);
        }

        #region Infoset


        //[JsonIgnore, XmlIgnore, ScriptIgnore,SoapIgnore]
        //public virtual Infoset Infoset { get; set; }
        //[JsonIgnore, XmlIgnore, ScriptIgnore, SoapIgnore]
        //public virtual string Data
        //{
        //    get { return Infoset.Data; }
        //    set { Infoset.Data = value; }
        //}

        //public NbEntityBase()
        //{
        //    Infoset = new Infoset();
        //}

        //protected TProperty Retrieve<TProperty>(Expression<Func<T, TProperty>> targetExpression)
        //{
        //    return InfosetHelper.Retrieve(this, targetExpression);
        //}
        //protected TProperty Retrieve<TPart, TProperty>(Expression<Func<TPart, TProperty>> targetExpression) where TPart : T
        //{
        //    var propertyInfo = ReflectionHelper<TPart>.GetPropertyInfo(targetExpression);
        //    var name = propertyInfo.Name;
        //    return InfosetHelper.Retrieve<T, TPk, TProperty>(this, name);
        //}

        //protected TProperty Retrieve<TProperty>(
        //    Expression<Func<T, TProperty>> targetExpression,
        //    Func<T, TProperty> defaultExpression)
        //{

        //    return InfosetHelper.Retrieve(this, targetExpression, defaultExpression);
        //}

        //protected TProperty Retrieve<TProperty>(
        //    Expression<Func<T, TProperty>> targetExpression,
        //    TProperty defaultValue)
        //{

        //    return InfosetHelper.Retrieve(this, targetExpression, (Func<T, TProperty>) (x => defaultValue));
        //}
        //protected void Store<TProperty>(
        //    Expression<Func<T, TProperty>> targetExpression,
        //    TProperty value)
        //{

        //    InfosetHelper.Store(this, targetExpression, value);
        //}
        //protected void Store<TPart, TProperty>(
        //   Expression<Func<TPart, TProperty>> targetExpression,
        //   TProperty value)
        //{
        //    var propertyInfo = ReflectionHelper<TPart>.GetPropertyInfo(targetExpression);
        //    var name = propertyInfo.Name;

        //    InfosetHelper.Store(this, name, value);
        //}

        #endregion
    }

    //aggregate root entity
    public abstract class NbEntity<T> : NbEntityBase<T, Guid> where T : class ,INbEntity<Guid>
    {
    }

    //not root entity
    public abstract class NbEntityInt<T> : NbEntityBase<T, int> where T : NbEntityInt<T>
    {
    }

    //value object
    public abstract class NbValueObject<T> : IEquatable<T> where T : NbValueObject<T>
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            T other = obj as T;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            IEnumerable<FieldInfo> fields = GetFields();

            int startValue = 17;
            int multiplier = 59;

            int hashCode = startValue;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value != null)
                    hashCode = hashCode * multiplier + value.GetHashCode();
            }

            return hashCode;
        }

        public virtual bool Equals(T other)
        {
            if (other == null)
                return false;

            Type t = GetType();
            Type otherType = other.GetType();

            if (t != otherType)
                return false;

            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                object value1 = field.GetValue(other);
                object value2 = field.GetValue(this);

                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (!value1.Equals(value2))
                    return false;
            }

            return true;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            Type t = GetType();

            List<FieldInfo> fields = new List<FieldInfo>();

            while (t != typeof(object))
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));

                t = t.BaseType;
            }

            return fields;
        }

        public static bool operator ==(NbValueObject<T> x, NbValueObject<T> y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(NbValueObject<T> x, NbValueObject<T> y)
        {
            return !(x == y);
        }
    }

    //demo for use NbValueObject
    /// <summary>
    /// 自定义的时间段模型，可用于有开始时间和结束时间的场合
    /// 例如课节等
    /// </summary>
    public class DateTimeRange : NbValueObject<DateTimeRange>
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public DateTimeRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
        public DateTimeRange(DateTime start, TimeSpan duration)
        {
            Start = start;
            End = start.Add(duration);
        }
        protected DateTimeRange() { }
        public DateTimeRange NewEnd(DateTime newEnd)
        {
            return new DateTimeRange(this.Start, newEnd);
        }
        public DateTimeRange NewDuration(TimeSpan newDuration)
        {
            return new DateTimeRange(this.Start, newDuration);
        }
        public DateTimeRange NewStart(DateTime newStart)
        {
            return new DateTimeRange(newStart, this.End);
        }

        public static DateTimeRange CreateMinuteRange(DateTime startDate, int minutes)
        {
            return new DateTimeRange(startDate, startDate.AddMinutes(minutes));
        }
        public static DateTimeRange CreateHourRange(DateTime startDate, int hour)
        {
            return new DateTimeRange(startDate, startDate.AddHours(hour));
        }
        public static DateTimeRange CreateOneDayRange(DateTime day)
        {
            return new DateTimeRange(day, day.AddDays(1));
        }
        public static DateTimeRange CreateOneWeekRange(DateTime startDay)
        {
            return new DateTimeRange(startDay, startDay.AddDays(7));
        }

        /// <summary>
        /// 保留时间，把日期部分自动修正为指定的时间的日期
        /// </summary>
        /// <param name="dayDate"></param>
        /// <returns></returns>
        public DateTimeRange FixDateTimeRangeOfSomeDay(DateTime dayDate)
        {
            var defaultDayDate = dayDate.Date;
            var startFix = defaultDayDate.Add(this.Start.TimeOfDay);
            var endFix = defaultDayDate.Add(this.End.TimeOfDay);
            var dateTimeRange = new DateTimeRange(startFix, endFix);
            return dateTimeRange;
        }

        ///// <summary>
        ///// 只保留时间，日期部分自动修正为2000，1，1
        ///// </summary>
        ///// <returns></returns>
        //public DateTimeRange FixDateTimeRangeOfDefaultDay()
        //{
        //    var fixDateTimeRangeOfSomeDay = FixDateTimeRangeOfSomeDay(new DateTime().GetDefaultDate().Date);
        //    return fixDateTimeRangeOfSomeDay;
        //}


        /// <summary>
        /// 时间间隔的分钟数部分。
        /// </summary>
        /// <returns></returns>
        public int DurationInMinutes()
        {
            return (End - Start).Minutes;
        }
        /// <summary>
        /// 查看两个时间段是否重叠
        /// </summary>
        /// <param name="dateTimeRange"></param>
        /// <returns></returns>
        public bool Overlaps(DateTimeRange dateTimeRange)
        {
            return this.Start < dateTimeRange.End &&
                this.End > dateTimeRange.Start;
        }
    }
}