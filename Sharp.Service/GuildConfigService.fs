namespace Sharp.Service

open Sharp.Data

module GuildConfigService =

    let addConfig guildId modChannelId =
        GuildConfigData.addConfig guildId modChannelId '-' false |> Operations.handleResultUnit
        
    let getConfig guildId =
        let result = GuildConfigData.getConfig guildId
        match result with
        |Ok config -> config
        |Error _ -> sprintf "no config was found for '%A'" guildId |> failwith

    let getAllConfigs =
        GuildConfigData.getAllConfigs |> Operations.handleResultList

    let setMessageLog guildId messagelog =
        GuildConfigData.setMessagelog guildId messagelog |> Operations.handleResultUnit

    let getMessageLog guildId =
        getConfig(guildId).messagelog
        
    let setModChannel guildId modChannel =
        GuildConfigData.setModChannel guildId modChannel |> Operations.handleResultUnit

    let getModChannel guildId =
        getConfig(guildId).modChannelId
        
    let setPrefix guildId prefix =
        GuildConfigData.setPrefix guildId prefix |> Operations.handleResultUnit
            
    let getPrefix guildId =
        getConfig(guildId).prefix