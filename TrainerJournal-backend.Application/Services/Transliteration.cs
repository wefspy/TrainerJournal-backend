using System.Text;

namespace TrainerJournal_backend.Application.Services;

public class Transliteration
{
    private static readonly Dictionary<char, string> TransliterationMap = new Dictionary<char, string>
    {
        {'А', "A"}, {'Б', "B"}, {'В', "V"}, {'Г', "G"}, {'Д', "D"},
        {'Е', "E"}, {'Ё', "E"}, {'Ж', "Zh"}, {'З', "Z"}, {'И', "I"},
        {'Й', "Y"}, {'К', "K"}, {'Л', "L"}, {'М', "M"}, {'Н', "N"},
        {'О', "O"}, {'П', "P"}, {'Р', "R"}, {'С', "S"}, {'Т', "T"},
        {'У', "U"}, {'Ф', "F"}, {'Х', "Kh"}, {'Ц', "Ts"}, {'Ч', "Ch"},
        {'Ш', "Sh"}, {'Щ', "Shch"}, {'Ъ', ""}, {'Ы', "Y"}, {'Ь', ""},
        {'Э', "E"}, {'Ю', "Yu"}, {'Я', "Ya"},
        {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"}, {'д', "d"},
        {'е', "e"}, {'ё', "e"}, {'ж', "zh"}, {'з', "z"}, {'и', "i"},
        {'й', "y"}, {'к', "k"}, {'л', "l"}, {'м', "m"}, {'н', "n"},
        {'о', "o"}, {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"},
        {'у', "u"}, {'ф', "f"}, {'х', "kh"}, {'ц', "ts"}, {'ч', "ch"},
        {'ш', "sh"}, {'щ', "shch"}, {'ъ', ""}, {'ы', "y"}, {'ь', ""},
        {'э', "e"}, {'ю', "yu"}, {'я', "ya"}
    };

    public static string ConvertToTransliteration(string input)
    {
        var sb = new StringBuilder();

        foreach (var c in input)
        {
            var exist = TransliterationMap.TryGetValue(c, out var transliteration);
            if (exist)
            {
                sb.Append(transliteration);
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }
}