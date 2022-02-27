using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Conduit.Shared.Validation;

[Serializable]
public sealed class LowerCaseSerializableError : Dictionary<string, object>
{
    public LowerCaseSerializableError(
        ModelStateDictionary modelState)
    {
        if (modelState.IsValid)
        {
            return;
        }

        foreach (var (key, value) in modelState)
        {
            var keyPath = GetPath(key);
            var dictionary = GetDictionaryForPath(keyPath);
            var errors = value.Errors;
            SetErrors(errors, dictionary, keyPath.Last());
        }
    }

    private static void SetErrors(
        ModelErrorCollection errors,
        IDictionary<string, object> dictionary,
        string targetKey)
    {
        if (errors.Count > 0)
        {
            var errorMessages = errors.Where(x =>
                    string.IsNullOrWhiteSpace(x.ErrorMessage) == false)
                .Select(x => x.ErrorMessage).ToArray();

            dictionary[targetKey] = errorMessages;
        }
    }

    private Dictionary<string, object> GetDictionaryForPath(
        string[] keyPath)
    {
        if (keyPath.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(keyPath));
        }

        if (keyPath.Length == 1)
        {
            return this;
        }

        var dictionary = (Dictionary<string, object>)this;

        foreach (var key in keyPath)
        {
            var obj = dictionary.GetValueOrDefault(key);
            if (obj is Dictionary<string, object> nextDictionary)
            {
                dictionary = nextDictionary;
            }
            else if (obj is IEnumerable<string>)
            {
                return this;
            }
            else if (obj is null)
            {
                nextDictionary = new();
                dictionary[key] = nextDictionary;
            }
        }

        return dictionary;
    }

    private static string[] GetPath(
        string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentOutOfRangeException(nameof(key));
        }

        var keyPath = key.Split('.').Select(LowerCase).ToArray();

        return keyPath;
    }

    private static string LowerCase(
        string subKey)
    {
        var subKeyArray = subKey.ToArray();
        subKeyArray[0] = char.ToLower(subKeyArray[0]);
        return new(subKeyArray);
    }
}