const $Auth = (policyName) => abp.auth.isGranted(policyName);
const $auth = {
    reseller: {
        level: {
            default: $Auth('Reseller_Level'),
            create: $Auth('Reseller_Level_Create'),
            update: $Auth('Reseller_Level_Update'),
            delete: $Auth('Reseller_Level_Delete')
        },
        list: {
            default: $Auth('Reseller_Level'),
            create: $Auth('Reseller_Level_Create'),
            update: $Auth('Reseller_Level_Update'),
            delete: $Auth('Reseller_Level_Delete')
        }
    },
    finance: {
        bankCard: {
            default: $Auth('Finance_BankCard'),
            create: $Auth('Finance_BankCard_Create'),
            update: $Auth('Finance_BankCard_Update'),
            delete: $Auth('Finance_BankCard_Delete')
        }
    },
    system: {
        role: {
            default: $Auth('Identity_Roles'),
            create: $Auth('Identity_Roles_Create'),
            update: $Auth('Identity_Roles_Update'),
            delete: $Auth('Identity_Roles_Delete'),
            permissions: $Auth('Identity_Roles_ManagePermissions')
        },
        user: {
            default: $Auth('Identity_Users'),
            create: $Auth('Identity_Users_Create'),
            update: $Auth('Identity_Users_Update'),
            delete: $Auth('Identity_Users_Delete'),
            permissions: $Auth('Identity_Users_ManagePermissions')
        }
    }
}

