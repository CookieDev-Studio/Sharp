namespace Sharp.Service

open Sharp.Data

module LinkService =

    let addLinkRolePair guildId roleId linkCode uses =
        LinkData.addLinkRolePair guildId roleId linkCode uses |> Operations.handleResultUnit

    let updateUses linkCode uses =
        LinkData.updateUses linkCode uses |> Operations.handleResultUnit

    let getLinkRolePairs guildId =
        LinkData.getLinkRolePairs guildId |> Operations.handleResultList
    