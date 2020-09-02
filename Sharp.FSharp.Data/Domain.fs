namespace Sharp.FSharp.Data

open NpgsqlTypes
open FSharp.Data

type Message =
    { guildId : uint64
      channelId : uint64 
      userId : uint64
      message : string
      date : NpgsqlDate }

type ConnectionString = JsonProvider<"../Sharp.Data/SqlConnection.json">