// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.9
// 

using Colyseus.Schema;

public partial class BloPosition : Schema {
	[Type(0, "string")]
	public string id_misi = default(string);

	[Type(1, "string")]
	public string id_object = default(string);

	[Type(2, "number")]
	public float lat = default(float);

	[Type(3, "number")]
	public float lng = default(float);

	[Type(4, "string")]
	public string warna = default(string);

	[Type(5, "string")]
	public string type = default(string);
}

