// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.9
// 

using Colyseus.Schema;

public partial class StateTFGSingle : Schema {
	[Type(0, "map", typeof(MapSchema<EntityTFGSingle>))]
	public MapSchema<EntityTFGSingle> entities = new MapSchema<EntityTFGSingle>();

	[Type(1, "map", typeof(MapSchema<EntityCamera>))]
	public MapSchema<EntityCamera> camera = new MapSchema<EntityCamera>();

	[Type(2, "map", typeof(MapSchema<WaktuTFG>))]
	public MapSchema<WaktuTFG> waktu = new MapSchema<WaktuTFG>();

	[Type(3, "map", typeof(MapSchema<ModeControl>))]
	public MapSchema<ModeControl> modeC = new MapSchema<ModeControl>();

	[Type(4, "map", typeof(MapSchema<dataTFG>), "dataTFG")]
	public MapSchema<dataTFG> dataTFG = new MapSchema<dataTFG>();

	[Type(5, "map", typeof(MapSchema<tools>), "tools")]
	public MapSchema<tools> tools = new MapSchema<tools>();

	[Type(6, "map", typeof(MapSchema<players>), "players")]
	public MapSchema<players> players = new MapSchema<players>();

	[Type(7, "map", typeof(MapSchema<percepatan>), "percepatan")]
	public MapSchema<percepatan> percepatan = new MapSchema<percepatan>();

	[Type(8, "map", typeof(MapSchema<WaktuKogas>))]
	public MapSchema<WaktuKogas> waktuKogas = new MapSchema<WaktuKogas>();
}

