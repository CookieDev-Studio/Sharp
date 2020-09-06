namespace Sharp.FSharp.Service

open System
open NpgsqlTypes
open Sharp.FSharp.Data

module StrikeService =
    let addStrike guildId userId modId reason (dateTime : DateTime) =
        StrikeData.AddStrike guildId userId modId reason (NpgsqlDateTime.ToNpgsqlDateTime dateTime)