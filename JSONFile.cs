using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJSON {
    public partial class JSONNode {
        public virtual JSONFile AsFile => new JSONFile($"{Guid.NewGuid()}.json").Append(this);
        public bool IsEmpty => Children.Count() == 0;
    }
    public class JSONFile : JSONObject {
        /// <summary>Can actually be absolute or relative path, location of file</summary>
        public readonly string RelativePath = $"{Guid.NewGuid()}.json"; // <= If the JSONNode is not a JSONFile, it will save it as a random named file
        /// <summary>Get only: Absolute path based on relative path (which can also be absolute)</summary>
        public string AbsolutePath => Path.Combine(Environment.CurrentDirectory, RelativePath);
        /// <summary>Encoding used when reading/writing</summary>
        public Encoding Encoding = Encoding.UTF8;

        public JSONFile(string path) => RelativePath = path;

        /// <summary>Load the File</summary>
        /// <returns>Current instance</returns>
        /// <exception cref="FormatException">Thrown when the file failed to parse</exception>
        public JSONFile Load() {
            if (!JSON.TryParse(File.Exists(AbsolutePath) ? File.ReadAllText(AbsolutePath) : "{}", out JSONNode node))
                throw new FormatException("JSON File is not in valid format! (Parse error)");
            Clear();
            return Append(node);
        }

        /// <summary>Loads the file asynchronously</summary>
        /// <param name="bufferSize">The buffer size, keep at default if you don't know what this means</param>
        /// <returns>Current instance</returns>
        /// <exception cref="FormatException">Thrown when the file failed to parse</exception>
        public async Task<JSONFile> LoadAsync(int bufferSize = 0x1000) {
            FileInfo fileInfo = new FileInfo(AbsolutePath);
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();

            using (FileStream sourceStream = new FileStream(fileInfo.FullName,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: bufferSize, useAsync: true
            )) {
                StringBuilder builder = new StringBuilder();

                byte[] buffer = new byte[bufferSize];
                int numRead;
                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0) {
                    string text = Encoding.GetString(buffer, 0, numRead);
                    builder.Append(text);
                }
                if (!JSON.TryParse(builder.ToString(), out JSONNode node))
                    throw new FormatException("JSON File is not in valid format! (Parse error)");

                Clear();
                return Append(node);
            }

        }

        /// <summary>Save the file</summary>
        /// <param name="formatted">Should the output be pretty printed?</param>
        /// <returns>Current instance</returns>
        public JSONFile Save(bool formatted = false) {
            FileInfo fileInfo = new FileInfo(AbsolutePath);
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();
            File.WriteAllText(AbsolutePath, ToString(formatted ? 2 : 0));
            return this;
        }

        /// <summary>Saves the file with a different name</summary>
        /// <param name="path">Path to the file, can be absolute or relative (start with '/' for absolute)</param>
        /// <param name="formatted">Should the output be pretty printed?</param>
        /// <returns>Current instance</returns>
        public JSONFile SaveAs(string path, bool formatted = false) {
            FileInfo fileInfo = new FileInfo(Path.Combine(Environment.CurrentDirectory, path));
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();
            File.WriteAllText(fileInfo.FullName, ToString(formatted ? 2 : 0));
            return this;
        }

        /// <summary>Saves current data asynchronously</summary>
        /// <param name="formatted">Should the file be pretty printed?</param>
        /// <param name="bufferSize">The buffer size, keep at default if you don't know what this means</param>
        /// <returns>Current instance</returns>
        public async Task<JSONFile> SaveAsync(bool formatted = false, int bufferSize = 0x1000) {
            FileInfo fileInfo = new FileInfo(AbsolutePath);
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();

            byte[] buffer = Encoding.GetBytes(ToString(formatted ? 2 : 0));

            using (FileStream sourceStream = new FileStream(fileInfo.FullName,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: bufferSize, useAsync: true)) {
                await sourceStream.WriteAsync(buffer, 0, buffer.Length);
            };
            return this;
        }

        /// <summary>Saves current data asynchronously</summary>
        /// <param name="path">Path to the file, can be absolute or relative (start with '/' for absolute)</param>
        /// <param name="formatted">Should the file be pretty printed?</param>
        /// <param name="bufferSize">The buffer size, keep at default if you don't know what this means</param>
        /// <returns>Current instance</returns>
        public async Task<JSONFile> SaveAsAsync(string path, bool formatted = false, int bufferSize = 0x1000) {
            FileInfo fileInfo = new FileInfo(Path.Combine(Environment.CurrentDirectory, path));
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();

            byte[] buffer = Encoding.GetBytes(ToString(formatted ? 2 : 0));
            using (FileStream sourceStream = new FileStream(fileInfo.FullName,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: bufferSize, useAsync: true)) {
                await sourceStream.WriteAsync(buffer, 0, buffer.Length);
            };
            return this;
        }

        /// <summary>Append nodes to the file</summary>
        /// <param name="node">The JSONObject to append</param>
        /// <returns>Current instance</returns>
        public JSONFile Append(JSONNode node) {
            foreach (KeyValuePair<string, JSONNode> pair in node)
                Add(pair.Key, pair.Value);
            return this;
        }


        /// <summary>Clears the data in the file</summary>
        /// <returns>Current instance</returns>
        public new JSONFile Clear() {
            base.Clear();
            return this;
        }
    }
}
