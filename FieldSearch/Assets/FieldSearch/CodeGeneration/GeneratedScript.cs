namespace CodeGeneration.Data
{
    /// <summary>
    /// Data class for Generated Script
    /// </summary>
    public class GeneratedScript
    {
        public GeneratedScript(string fileName, string scriptStr)
        {
            this.fileName = fileName;
            this.scriptStr = scriptStr;
        }

        public string fileName;
        public string scriptStr;
    }
}
