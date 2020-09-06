namespace Sharp.Domain

open NpgsqlTypes
open FSharp.Data

type internal ConnectionString = JsonProvider<"../Sharp.Data/SqlConnection.json">

type GuildId = GuildId of uint64
type ChannelId = ChannelId of uint64
type ModChannelId = ModChannelId of uint64
type RoleId = RoleId of uint64
type ModId = ModId of uint64
type UserId = UserId of uint64

type Message =
    { guildId : uint64
      channelId : uint64 
      userId : uint64
      message : string
      date : NpgsqlDateTime }

type Strike =
    { id : int
      guildId : GuildId 
      userId : UserId
      modId : ModId
      reason : string
      date : NpgsqlDateTime }

type GuildConfig =
    { guildId : GuildId
      modChannelId : ModChannelId
      prefix : char
      messagelog : bool }

type LinkRolePair =
    { guildId : GuildId
      roleId : RoleId
      linkCode : string
      uses : int }