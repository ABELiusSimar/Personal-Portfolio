namespace MultiGuess
{
    internal class VocabularyChecker : IVocabularyChecker
    {
        List<string>? stringList;

        public VocabularyChecker()
        {
            StreamReader? reader = null;
            try
            {
                reader = new StreamReader(new FileStream("wordlist.txt", FileMode.OpenOrCreate));

                var content = reader.ReadToEndAsync();

                stringList = content.Result.Split('\n').ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                reader?.Dispose();
            }
        }

        public bool Exists(string word)
        {
            return stringList?.Contains(word) == true;
        }
    }
}
