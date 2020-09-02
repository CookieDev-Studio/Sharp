namespace Sharp.FSharp.Data

open Npgsql.FSharp
open System

module MessageData =
    let private getDB queryString  = 
        Operations.getConnectionString
        |> Sql.connect
        |> Sql.query queryString

    let executeQuery func =
        getDB >> Sql.execute func

    let executeNonQuery =
        getDB >> Sql.executeNonQuery

    let getMessages (guildId : uint64) =
        sprintf "SELECT * FROM message WHERE guild_id = '%i'" guildId 
        |> executeQuery (fun read ->
            { guildId = read.string "guild_id" |> UInt64.Parse
              channelId = read.string "channel_id" |> UInt64.Parse
              userId = read.string "user_id" |> UInt64.Parse
              message = read.text "message"
              date = read.date "date_time" })

    let addMessage (guildId : uint64) (modChannelId : uint64) (userId : uint64) (dateTime : DateTime) message =
        executeNonQuery 
            (sprintf "SELECT add_message('%i', '%i', '%i', E'%s', '%A')"
                guildId
                modChannelId
                userId
                message
                dateTime)