namespace Sharp.FSharp.Domain

open NpgsqlTypes
open FSharp.Data

type internal ConnectionString = JsonProvider<"../Sharp.Data/SqlConnection.json">

type Message =
    { guildId : uint64
      channelId : uint64 
      userId : uint64
      message : string
      date : NpgsqlDateTime }


type GuildId = GuildId of uint64
type ChannelId = ChannelId of uint64
type ModChannelId = ModChannelId of uint64
type ModId = ModId of uint64
type UserId = UserId of uint64

type Strike =
    { id : int
      guildId : GuildId 
      userId : UserId
      modId : ModId
      reason : string
      date : NpgsqlDateTime }