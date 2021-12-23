using FoodPlanner.Common;
using YamlDotNet.RepresentationModel;

namespace FoodPlanner.Data
{
    public class RecipeIO
    {
        private const string NameKey = "namn";
        private const string DescriptionKey = "beskrivning";
        private const string ServingsKey = "portioner";
        private const string IngredientsKey = "ingredienser";
        private const string TagsKey = "nyckelord";

        public static Recipe ReadRecipeFile(string filePath)
        {
            var text = File.ReadAllText(filePath);

            return CreateRecipeFromYaml(text, Path.GetDirectoryName(filePath) ?? string.Empty);
        }

        public static Recipe CreateRecipeFromYaml(string yamlText, string dirName)
        {
            using (var reader = new StringReader(yamlText))
            {
                var yaml = new YamlStream();
                yaml.Load(reader);

                var root = (YamlMappingNode)yaml.Documents[0].RootNode;
                ValidateAllExpectedKeysArePresent(root, NameKey, DescriptionKey, ServingsKey, IngredientsKey, TagsKey);

                var name = GetStringValueFromMapping(root, NameKey);
                var description = Path.Combine(dirName, GetStringValueFromMapping(root, DescriptionKey));
                var servings = GetIntValueFromMapping(root, ServingsKey);
                var ingredients = GetStringArrayValueFromMapping(root, IngredientsKey);
                var tags = GetStringArrayValueFromMapping(root, TagsKey);

                return new Recipe(name, description, servings, ingredients, tags);
            }
        }

        private static void ValidateAllExpectedKeysArePresent(YamlMappingNode mapping, params string[] keys)
        {
            if (mapping == null || mapping.Children.Count != keys.Length
                || !keys.All(k => mapping.Children.Keys.Contains(new YamlScalarNode(k))))
            {
                throw new ArgumentException("The required keys are not present.");
            }
        }

        private static string GetStringValueFromMapping(YamlMappingNode mapping, string key)
        {
            if (mapping.Children.TryGetValue(new YamlScalarNode(key), out YamlNode? node)
                && node is YamlScalarNode scalarNode
                && scalarNode.Value is string value)
            {
                return value;
            }
            else
            {
                throw new ArgumentException($"No string value defined for key '{key}'");
            }
        }

        private static int GetIntValueFromMapping(YamlMappingNode mapping, string key)
        {
            if (mapping.Children.TryGetValue(new YamlScalarNode(key), out YamlNode? node)
                && node is YamlScalarNode scalarNode
                && scalarNode.Value is string value
                && Int32.TryParse(value, out int intValue))
            {
                return intValue;
            }
            else
            {
                throw new ArgumentException($"No integer value defined for key '{key}'");
            }
        }

        private static string[] GetStringArrayValueFromMapping(YamlMappingNode mapping, string key)
        {
            if (mapping.Children.TryGetValue(new YamlScalarNode(key), out YamlNode? node)
                && node is YamlSequenceNode sequenceNode
                && sequenceNode.Children.All(n => n is YamlScalarNode scalarNode && scalarNode.Value is string))
            {
                return sequenceNode.Children.Select(n => ((YamlScalarNode)n).Value).OfType<string>().ToArray();
            }
            else
            {
                throw new ArgumentException($"The '{key}' key is not mapped to a sequence of strings.");
            }
        }
    }
}
