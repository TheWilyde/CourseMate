namespace CourseMate.Models.HelpingClasses
{
    public static class EnumHelper
    {
        public static int ConverEnumToValue<TEnum>(this TEnum enumValue) where TEnum : Enum
            => Convert.ToInt32(enumValue);
    }

}
