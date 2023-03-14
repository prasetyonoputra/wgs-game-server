// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.9
// 

using Colyseus.Schema;

public partial class Weather : Schema {
	[Type(0, "string")]
	public string id_weather = default(string);

	[Type(1, "string")]
	public string coordinate = default(string);

	[Type(2, "string")]
	public string jenis = default(string);
}

