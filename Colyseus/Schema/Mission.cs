// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.9
// 

using Colyseus.Schema;

public partial class Mission : Schema {
	[Type(0, "string")]
	public string id = default(string);

	[Type(1, "string")]
	public string idPrimary = default(string);

	[Type(2, "string")]
	public string jenis = default(string);

	[Type(3, "string")]
	public string id_user = default(string);

	[Type(4, "string")]
	public string tgl_mulai = default(string);

	[Type(5, "string")]
	public string id_object = default(string);

	[Type(6, "string")]
	public string properties = default(string);

	[Type(7, "number")]
	public float status = default(float);

	[Type(8, "boolean")]
	public bool isSelected = default(bool);

	[Type(9, "boolean")]
	public bool panahSelected = default(bool);

	[Type(10, "boolean")]
	public bool used = default(bool);

	[Type(11, "string")]
	public string missionDefault = default(string);
}

