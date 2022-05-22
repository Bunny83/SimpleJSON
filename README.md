# Simple JSON

[![openupm](https://img.shields.io/npm/v/com.bunny83.simplejson?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.bunny83.simplejson/)
[![GitHub issues](https://img.shields.io/github/issues/Battlehub0x/SimpleJSON)](https://github.com/Battlehub0x/SimpleJSON/issues)
[![GitHub license](https://img.shields.io/github/license/Battlehub0x/SimpleJSON?label=license)](https://github.com/Battlehub0x/SimpleJSON/blob/main/LICENSE)

SimpleJSON mainly has been written as a simple JSON parser. It can build a JSON string from the node-tree, or generate a node tree from any valid JSON string.

Written by Bunny83 2012-06-09

SimpleJSONBinary is an extension of the SimpleJSON framework to provide methods to serialize a JSON object tree into a compact binary format. Optionally the binary stream can be compressed with the SharpZipLib when using the define "USE_SharpZipLib"

Those methods where originally part of the framework but since it's rarely used I've extracted this part into this seperate module file.

You can use the define "SimpleJSON_ExcludeBinary" to selectively disable this extension without the need to remove the file from the project.

If you want to use compression when saving to file / stream / B64 you have to include SharpZipLib ( http://www.icsharpcode.net/opensource/sharpziplib/ ) in your project and define "USE_SharpZipLib" at the top of the file

SimpleJSONUnity is a Unity extension for the SimpleJSON framework. It does only work together with the SimpleJSON.cs It provides several helpers and conversion operators to serialize/deserialize common Unity types such as Vector2/3/4, Rect, RectOffset, Quaternion and Matrix4x4 as JSONObject or JSONArray. This extension will add 3 static settings to the JSONNode class: ( VectorContainerType, QuaternionContainerType, RectContainerType ) which control what node type should be used for serializing the given type. So a Vector3 as array would look like [12,32,24] and {"x":12, "y":32, "z":24} as object.

## Installation

The easiest way to install is to download and open the [Installer Package](https://package-installer.glitch.me/v1/installer/OpenUPM/com.bunny83.simplejson?registry=https%3A%2F%2Fpackage.openupm.com&scope=com.bunny83)

It runs a script that installs Package Utils via a scoped registry.

Afterwards Package Utils is listed in the Package Manager (under My Registries) and can be installed and updated from there.