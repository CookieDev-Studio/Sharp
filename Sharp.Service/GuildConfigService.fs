namespace Sharp.Service

open Sharp.Data

module GuildConfigService =

    let addConfig guildId modChannelId =
        GuildConfigData.addConfigAsync guildId modChannelId '-' false |> Operations.handleResultUnitAsync

    let private getConfig guildId =
        async {
            let! result = GuildConfigData.getConfigAsync guildId
            match result with
            |Ok config -> return config
            |Error _ -> return sprintf "no config was found for '%A'" guildId |> failwith }

    let getConfigAsync guildId = getConfig guildId |> Async.StartAsTask

    let getAllConfigsAsync =
        GuildConfigData.getAllConfigsAsync |> Operations.handleResultListAsync

    let setMessageLog guildId messagelog =
        GuildConfigData.setMessagelogAsync guildId messagelog
        |> Operations.handleResultUnitAsync

    let getMessageLog guildId =
        async {
            let! config = getConfig guildId
            return config.messagelog }
        |> Async.StartAsTask
        
    let setModChannel guildId modChannel =
        GuildConfigData.setModChannelAsync guildId modChannel 
        |> Operations.handleResultUnitAsync

    let getModChannelAsync guildId =
        async {
            let! config = getConfig guildId
            return config.modChannelId }
        |> Async.StartAsTask
        
    let setPrefix guildId prefix =
        GuildConfigData.setPrefixAsync guildId prefix 
        |> Operations.handleResultUnitAsync
            
    let getPrefixAsync guildId =
        async {
            let! config = getConfig guildId
            return config.prefix }
        |> Async.StartAsTask