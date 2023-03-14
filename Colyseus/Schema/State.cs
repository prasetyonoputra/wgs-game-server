// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.9
// 

using Colyseus.Schema;

public partial class State : Schema {
	[Type(0, "array", typeof(ArraySchema<string>), "string")]
	public ArraySchema<string> wastebasket = new ArraySchema<string>();

	[Type(1, "ref", typeof(World))]
	public World world = new World();

	[Type(2, "map", typeof(MapSchema<Entity>))]
	public MapSchema<Entity> entities = new MapSchema<Entity>();

	[Type(3, "map", typeof(MapSchema<ListTools>))]
	public MapSchema<ListTools> listTools = new MapSchema<ListTools>();

	[Type(4, "map", typeof(MapSchema<ListTexts>))]
	public MapSchema<ListTexts> listTexts = new MapSchema<ListTexts>();

	[Type(5, "map", typeof(MapSchema<Mission>))]
	public MapSchema<Mission> missions = new MapSchema<Mission>();

	[Type(6, "map", typeof(MapSchema<Weather>))]
	public MapSchema<Weather> weather = new MapSchema<Weather>();

	[Type(7, "map", typeof(MapSchema<Action2>))]
	public MapSchema<Action2> action = new MapSchema<Action2>();

	[Type(8, "map", typeof(MapSchema<TimeMedia>))]
	public MapSchema<TimeMedia> timeMedia = new MapSchema<TimeMedia>();

	[Type(9, "map", typeof(MapSchema<LogActivity>))]
	public MapSchema<LogActivity> logactivity = new MapSchema<LogActivity>();

	[Type(10, "map", typeof(MapSchema<LogSystem>))]
	public MapSchema<LogSystem> logsystem = new MapSchema<LogSystem>();

	[Type(11, "map", typeof(MapSchema<Formasi>))]
	public MapSchema<Formasi> formasi = new MapSchema<Formasi>();

	[Type(12, "map", typeof(MapSchema<players>), "players")]
	public MapSchema<players> players = new MapSchema<players>();

	[Type(13, "map", typeof(MapSchema<tools>), "tools")]
	public MapSchema<tools> tools = new MapSchema<tools>();

	[Type(14, "map", typeof(MapSchema<letter>), "letter")]
	public MapSchema<letter> letter = new MapSchema<letter>();

	[Type(15, "map", typeof(MapSchema<BloPosition>))]
	public MapSchema<BloPosition> blo = new MapSchema<BloPosition>();

	[Type(16, "map", typeof(MapSchema<usertools>), "usertools")]
	public MapSchema<usertools> usertools = new MapSchema<usertools>();
}

