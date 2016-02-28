#A simple JSON Parser / builder

It mainly has been written as a simple JSON parser. It can build a JSON string
from the node-tree, or generate a node tree from any valid JSON string.

If you want to use compression when saving to file / stream / B64 you have to include
SharpZipLib ( http://www.icsharpcode.net/opensource/sharpziplib/ ) in your project and
define "USE_SharpZipLib" at the top of the file

Written by Bunny83 
2012-06-09

Features / attributes:
- provides strongly typed node classes and lists / dictionaries
- provides easy access to class members / array items / data values
- the parser ignores data types. Each value is a string.
- only double quotes (") are used for quoting strings.
- values and names are not restricted to quoted strings. They simply add up and are trimmed.
- There are only 3 types: arrays(JSONArray), objects(JSONClass) and values(JSONData)
- provides "casting" properties to easily convert to / from those types:
int / float / double / bool
- provides a common interface for each node so no explicit casting is required.
- the parser try to avoid errors, but if malformed JSON is parsed the result is undefined


2012-12-17 Update:
- Added internal JSONLazyCreator class which simplifies the construction of a JSON tree
Now you can simple reference any item that doesn't exist yet and it will return a JSONLazyCreator
The class determines the required type by it's further use, creates the type and removes itself.
- Added binary serialization / deserialization.
- Added support for BZip2 zipped binary format. Requires the SharpZipLib ( http://www.icsharpcode.net/opensource/sharpziplib/ )
The usage of the SharpZipLib library can be disabled by removing or commenting out the USE_SharpZipLib define at the top
- The serializer uses different types when it comes to store the values. Since my data values
are all of type string, the serializer will "try" which format fits best. The order is: int, float, double, bool, string.
It's not the most efficient way but for a moderate amount of data it should work on all platforms.

2016-02-28 Update:
- Added JSON Syntax Validate
