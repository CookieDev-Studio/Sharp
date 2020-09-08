namespace Sharp.Data

open Sharp.Domain
open Npgsql.FSharp
open System

module GuildConfigData =

    let private parseConfig (read : RowReader) =
        { guildId = read.string "guild_id" |> UInt64.Parse |> GuildId
          modChannelId = read.string "mod_channel_id" |> UInt64.Parse |> ModChannelId
          prefix = (read.string "prefix").[0]
          messagelog = read.bool "message_log"}

    let addConfigAsync (GuildId guildId) (ModChannelId modChannelId) (prefix : char) (messagelog : bool) =
        sprintf "SELECT * FROM add_config('%i', '%i', '%c', '%b')"
            guildId modChannelId prefix messagelog
        |> Operations.executeNonQueryAsync

    let getConfigAsync (GuildId guildId) =
        sprintf "SELECT * FROM config
                 WHERE guild_id = '%i'"
            guildId
        |> Operations.executeRowAsync parseConfig

    let getAllConfigsAsync =
        sprintf "SELECT * FROM config"
        |> Operations.executeQueryAsync parseConfig

    let setMessagelogAsync (GuildId guildId) (messagelog : bool) =
        sprintf "SELECT * FROM set_message_log('%i', '%b')"
            guildId messagelog
        |> Operations.executeNonQueryAsync

    let setModChannelAsync (GuildId guildId) (ModChannelId modChannelId) =
           sprintf "SELECT * FROM set_mod_channel('%i', '%i')"
               guildId modChannelId
           |> Operations.executeNonQueryAsync

    let setPrefixAsync (GuildId guildId) (prefix : char) =
        sprintf "SELECT * FROM set_prefix('%i', '%c')"
            guildId prefix
        |> Operations.executeNonQueryAsync