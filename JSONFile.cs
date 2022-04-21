using System;
using System.IO;

namespace SimpleJSON
{
    public class JSONFile
    {
        public readonly string OriginalPath = string.Empty;
        public string Path { get; private set; } = string.Empty;
        public JSONNode Data = new JSONObject();
        public JSONNode this[string index]
        {
            get => Data[index];
            set => Data[index] = value;
        }

        public JSONFile(string path) {
            OriginalPath = path;
            Path = System.IO.Path.Combine(Environment.CurrentDirectory, path);
        }

        public JSONFile Load()
        {
            try { Data = JSONNode.Parse(File.ReadAllText(Path)); }
            catch { Data = new JSONObject(); }
            return this;
        }

        public void Save(bool formatted = false) {
            FileInfo fileInfo = new FileInfo(Path);
            fileInfo.Directory.Create();
            File.WriteAllText(Path, Data.ToString(formatted ? 2 : 0));
        }
    }
}
