namespace Sharp.Data

open Sharp.Domain
open System
open Npgsql.FSharp

module MessageData =

    let private parseMessage (read : RowReader ) =
        { guildId = read.string "guild_id" |> UInt64.Parse
          channelId = read.string "channel_id" |> UInt64.Parse
          userId = read.string "user_id" |> UInt64.Parse
          message = read.text "message"
          date = read.timestamptz "date_time" }

    let getMessages (GuildId guildId) =
        sprintf "SELECT * FROM message WHERE guild_id = '%i'" guildId 
        |> Operations.executeQuery parseMessage
            
        

    let addMessage (GuildId guildId) (ModChannelId modChannelId) (UserId userId) (dateTime : DateTime) message =
        sprintf "SELECT add_message('%i', '%i', '%i', E'%s', '%A')"
            guildId
            modChannelId
            userId
            message
            dateTime
        |> Operations.executeNonQuery