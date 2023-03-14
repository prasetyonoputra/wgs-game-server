// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.9
// 

using Colyseus.Schema;

public partial class letter : Schema {
	[Type(0, "string")]
	public string dari = default(string);

	[Type(1, "string")]
	public string dariKogas = default(string);

	[Type(2, "string")]
	public string typeLetter = default(string);

	[Type(3, "string")]
	public string tujuan = default(string);

	[Type(4, "string")]
	public string subject = default(string);

	[Type(5, "string")]
	public string message = default(string);

	[Type(6, "string")]
	public string property = default(string);

	[Type(7, "boolean")]
	public bool status = default(bool);

	[Type(8, "string")]
	public string createdDate = default(string);

	[Type(9, "string")]
	public string flag = default(string);
}

