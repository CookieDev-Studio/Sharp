namespace Sharp.Service

open Sharp.Data

module LinkService =

    let addLinkRolePair guildId roleId linkCode uses =
        LinkData.addLinkRolePairAsync guildId roleId linkCode uses |> Operations.handleResultUnitAsync

    let updateUses linkCode uses =
        LinkData.updateUsesAsync linkCode uses |> Operations.handleResultUnitAsync

    let getLinkRolePairsAsync guildId =
        LinkData.getLinkRolePairsAsync guildId |> Operations.handleResultListAsync
    