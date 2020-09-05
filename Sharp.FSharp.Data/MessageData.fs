namespace Sharp.FSharp.Data

open System

module MessageData =
    
    let getMessages (guildId : uint64) =
        sprintf "SELECT * FROM message WHERE guild_id = '%i'" guildId 
        |> Operations.executeQuery (fun read ->
            { guildId = read.string "guild_id" |> UInt64.Parse
              channelId = read.string "channel_id" |> UInt64.Parse
              userId = read.string "user_id" |> UInt64.Parse
              message = read.text "message"
              date = read.date "date_time" })
        

    let addMessage (guildId : uint64) (modChannelId : uint64) (userId : uint64) (dateTime : DateTime) message =
        Operations.executeNonQuery 
            (sprintf "SELECT add_message('%i', '%i', '%i', E'%s', '%A')"
                guildId
                modChannelId
                userId
                message
                dateTime)