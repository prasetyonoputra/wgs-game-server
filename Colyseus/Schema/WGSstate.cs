// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.9
// 

using Colyseus.Schema;

public partial class WGSstate : Schema {
	[Type(0, "array", typeof(ArraySchema<EntityState>))]
	public ArraySchema<EntityState> entityStates = new ArraySchema<EntityState>();
}

