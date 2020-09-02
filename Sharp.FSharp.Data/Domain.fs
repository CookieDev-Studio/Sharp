namespace Sharp.FSharp.Data

open NpgsqlTypes
open FSharp.Data

type internal ConnectionString = JsonProvider<"../Sharp.Data/SqlConnection.json">

type Message =
    { guildId : uint64
      channelId : uint64 
      userId : uint64
      message : string
      date : NpgsqlDate }

