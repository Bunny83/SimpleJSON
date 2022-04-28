using System;
using System.Collections.Generic;
using System.IO;
using SPath = System.IO.Path;

namespace SimpleJSON {
    public partial class JSONNode {
        public virtual JSONFile AsFile => this as JSONFile;
    }
    public class JSONFile : JSONObject {
        public string Path = string.Empty;
        public string SystemPath => SPath.Combine(Environment.CurrentDirectory, Path ?? Guid.NewGuid().ToString());

        public JSONFile(string path) => Path = path;

        public JSONFile Load() {
            if (!JSON.TryParse(File.ReadAllText(SystemPath), out JSONNode node))
                throw new FormatException("JSON File is not in valid format! (Parse error)");
            foreach (KeyValuePair<string, JSONNode> pair in node)
                Add(pair.Key, pair.Value);
            return this;
        }

        public JSONFile Save(bool formatted = false) {
            FileInfo fileInfo = new FileInfo(Path);
            fileInfo.Directory.Create();
            File.WriteAllText(Path, ToString(formatted ? 2 : 0));
            return this;
        }

        public JSONFile Save(string path, bool formatted = false) {
            if (string.IsNullOrEmpty(Path))
                return Save(formatted);
            FileInfo fileInfo = new FileInfo(SPath.Combine(Environment.CurrentDirectory, path));
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();
            File.WriteAllText(fileInfo.FullName, ToString(formatted ? 2 : 0));
            return this;
        }
    }
}
