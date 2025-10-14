public static class ValidationHelper
{
    public static bool HasAllFieldsFilled<T>(T obj)
    {
        if (obj == null)
            return false;

        var properties = typeof(T).GetProperties();

        foreach (var prop in properties)
        {
            var value = prop.GetValue(obj);

            if (value == null)
                return false;

            if (value is string str && string.IsNullOrWhiteSpace(str))
                return false;
        }

        return true;
    }
}
