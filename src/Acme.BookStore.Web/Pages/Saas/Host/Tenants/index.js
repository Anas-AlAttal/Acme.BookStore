// src/Acme.BookStore.Web/Pages/Saas/Host/Tenants/index.js
(function () {

    let l = abp.localization.getResource('Saas');
    let _tenantAppService = volo.saas.host.tenant;

    let _editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Saas/Host/Tenants/EditModal',
        modalClass: 'SaaSTenantEdit'
    });
    _editModal.onResult(function () {
        abp.notify.success(l('SavedSuccessfully'));
    });

    let _createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Saas/Host/Tenants/CreateModal',
        modalClass: 'SaaSTenantCreate'
    });
    _createModal.onResult(function () {
        abp.notify.success(l('CreatedSuccessfully'));
    });

    let _featuresModal = new abp.ModalManager(
        abp.appPath + 'FeatureManagement/FeatureManagementModal'
    );
    _featuresModal.onResult(function () {
        abp.notify.success(l('SavedSuccessfully'));
    });

    let _changeTenantPasswordModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Saas/Host/Tenants/SetPassword',
        modalClass: 'changeTenantPassword'
    });
    _changeTenantPasswordModal.onResult(function () {
        abp.notify.success(l('SavedSuccessfully'));
    });

    let _inviteUserModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Saas/Host/Tenants/InviteUser',
        modalClass: 'inviteTenantUser'
    });
    _inviteUserModal.onResult(function () {
        abp.notify.success(l('SavedSuccessfully'));
    });

    let _connectionStringsModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Saas/Host/Tenants/ConnectionStringsModal',
        modalClass: 'TenantConnectionStringManagement'
    });
    _connectionStringsModal.onResult(function () {
        abp.notify.success(l('SavedSuccessfully'));
    });

    let _dataTable = null;

    // ════════════════════════════════════════════════════════════
    //  Entity Actions — أضف BookStore Settings هنا
    // ════════════════════════════════════════════════════════════
    abp.ui.extensions.entityActions.get("saas.tenant").addContributor(
        function (actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted('Saas.Tenants.Update'),
                        action: function (data) {
                            _editModal.open({ id: data.record.id });
                        }
                    },
                    {
                        text: l('ConnectionStrings'),
                        visible: abp.setting.getBoolean(
                            "Volo.Saas.EnableTenantBasedConnectionStringManagement"
                        ) && abp.auth.isGranted('Saas.Tenants.ManageConnectionStrings'),
                        action: function (data) {
                            _connectionStringsModal.open({ id: data.record.id });
                        }
                    },
                    {
                        text: l('ApplyDatabaseMigrations'),
                        visible: function (record) {
                            return record.hasDefaultConnectionString &&
                                abp.auth.isGranted('Saas.Tenants.ManageConnectionStrings') &&
                                abp.setting.getBoolean(
                                    "Volo.Saas.EnableTenantBasedConnectionStringManagement"
                                );
                        },
                        action: function (data) {
                            _tenantAppService
                                .applyDatabaseMigrations(data.record.id)
                                .then(function () {
                                    abp.notify.info(
                                        l('DatabaseMigrationQueuedAndWillBeApplied')
                                    );
                                });
                        }
                    },
                    {
                        text: l('Features'),
                        visible: abp.auth.isGranted('Saas.Tenants.ManageFeatures'),
                        action: function (data) {
                            _featuresModal.open({
                                providerName: 'T',
                                providerKey: data.record.id,
                                providerKeyDisplayName: data.record.name
                            });
                        }
                    },
                    {
                        text: l('InviteUser'),
                        visible: abp.auth.isGranted('Saas.Tenants.Create'),
                        action: function (data) {
                            _inviteUserModal.open({ id: data.record.id });
                        }
                    },
                    {
                        text: l('SetPassword'),
                        visible: abp.auth.isGranted('Saas.Tenants.SetPassword'),
                        action: function (data) {
                            _changeTenantPasswordModal.open({ id: data.record.id });
                        }
                    },
                    {
                        text: l('ChangeHistory'),
                        visible: abp.auditLogging &&
                            abp.auth.isGranted(
                                'AuditLogging.ViewChangeHistory:Volo.Saas.Tenant'
                            ),
                        action: function (data) {
                            abp.auditLogging.openEntityHistoryModal(
                                "Volo.Saas.Tenants.Tenant",
                                data.record.id
                            );
                        }
                    },

                    // ✅ BookStore Settings — الزر الجديد
                    {
                        text: 'BookStore Settings',
                        icon: 'fa fa-cog',
                        visible: abp.auth.isGranted(
                            'BookStore.Settings.ManageTenant'
                        ),
                        //visible: true,
                        action: function (data) {
                            window.location.href =
                                '/Settings/TenantSettings/' + data.record.id;
                        }
                    },

                    {
                        text: l('Delete'),
                        visible: abp.auth.isGranted('Saas.Tenants.Delete'),
                        confirmMessage: function (data) {
                            return l(
                                'TenantDeletionConfirmationMessage',
                                data.record.name
                            );
                        },
                        action: function (data) {
                            _tenantAppService
                                .delete(data.record.id)
                                .then(function () {
                                    _dataTable.ajax.reloadEx();
                                    abp.notify.success(l('DeletedSuccessfully'));
                                });
                        }
                    }
                ]
            );
        }
    );

    // ════════════════════════════════════════════════════════════
    //  Table Columns — لم يتغير شيء
    // ════════════════════════════════════════════════════════════
    abp.ui.extensions.tableColumns.get("saas.tenant").addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions
                                .get("saas.tenant").actions.toArray()
                        }
                    },
                    {
                        title: l("TenantName"),
                        data: "name"
                    },
                    {
                        title: l("Edition"),
                        data: "editionName",
                        orderable: false
                    },
                    {
                        title: '<span data-toggle="tooltip" title="' +
                            l('EditionEndDateToolTip') + '">' +
                            l("EditionEndDateUtc") + '</span>',
                        data: "editionEndDateUtc",
                        orderable: false,
                        render: function (data, type, row) {
                            if (data < new Date().toISOString()) {
                                return '<span class="text-danger" data-toggle="tooltip" title="' +
                                    l('EditionEndDateToolTip') + '">' + data + '</span>';
                            } else {
                                return data;
                            }
                        }
                    },
                    {
                        title: l("ActivationState"),
                        data: "activationState",
                        render: function (data, type, row) {
                            switch (data) {
                                case 0:
                                    return l("Enum:TenantActivationState.Active");
                                case 1:
                                    return l("Enum:TenantActivationState.ActiveWithLimitedTime") +
                                        " (" +
                                        (new Date(Date.parse(row.activationEndDate)))
                                            .toLocaleString(abp.localization.currentCulture.name) +
                                        ")";
                                case 2:
                                    return l("Enum:TenantActivationState.Passive");
                                default:
                                    return null;
                            }
                        }
                    }
                ]
            );
        },
        0
    );

    $(function () {

        let _$wrapper = $('#TenantsWrapper');

        let getFilter = function () {
            let editionId = _$wrapper.find("select#tenant-edition-option").val();
            let activationState = _$wrapper.find("select#ActivationState").val();
            let expirationDateRange = $('#expirationDateRangePicker').val().split(' - ');
            let activationEndDateRange = $('#activationEndDateRangePicker').val().split(' - ');

            return {
                filter: _$wrapper.find("input.page-search-filter-text").val(),
                editionId: !editionId ? null : editionId,
                expirationDateMin: expirationDateRange.length > 1
                    ? expirationDateRange[0] : null,
                expirationDateMax: expirationDateRange.length > 1
                    ? expirationDateRange[1] : null,
                activationEndDateMin: activationEndDateRange.length > 1
                    ? activationEndDateRange[0] : null,
                activationEndDateMax: activationEndDateRange.length > 1
                    ? activationEndDateRange[1] : null,
                activationState: activationState
            };
        };

        _dataTable = _$wrapper.find('table').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                processing: true,
                serverSide: true,
                paging: true,
                scrollX: true,
                searching: false,
                scrollCollapse: true,
                order: [],
                ajax: abp.libs.datatables.createAjax(
                    _tenantAppService.getList, getFilter
                ),
                columnDefs: abp.ui.extensions.tableColumns
                    .get("saas.tenant").columns.toArray()
            })
        );

        _createModal.onResult(function () { _dataTable.ajax.reloadEx(); });
        _editModal.onResult(function () { _dataTable.ajax.reloadEx(); });

        $('#AbpContentToolbar button[name=CreateTenant]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });

        _$wrapper.find("form.page-search-form").submit(function (e) {
            e.preventDefault();
            _dataTable.ajax.reloadEx();
        });

        $("select#tenant-edition-option").change(function () {
            _dataTable.ajax.reloadEx();
        });

        $("input#ExpirationDateMax, input#ExpirationDateMin").change(function () {
            _dataTable.ajax.reloadEx();
        });

        $("select#ActivationState").change(function () {
            _dataTable.ajax.reloadEx();
        });

        $("#AdvancedFilterSectionToggler").click(function () {
            $("#AdvancedFilterSection").toggle();
            var iconCss = $("#AdvancedFilterSection").is(":visible")
                ? "fa ms-1 fa-angle-up"
                : "fa ms-1 fa-angle-down";
            $(this).find("i").attr("class", iconCss);
        });

        $('#expirationDateRangePicker, #activationEndDateRangePicker').change(function () {
            _dataTable.ajax.reloadEx();
        });

        var daterangepicker = $('#TenantsWrapper .date-range-picker');
        daterangepicker.daterangepicker({
            autoUpdateInput: false,
            locale: {
                cancelLabel: l('Clear'),
                applyLabel: l('Apply'),
                format: 'MM/DD/YYYY'
            }
        });

        daterangepicker.on('apply.daterangepicker', function (ev, picker) {
            $(this).val(
                picker.startDate.format('MM/DD/YYYY') +
                ' - ' +
                picker.endDate.format('MM/DD/YYYY')
            );
            $(this).trigger("change");
        });

        daterangepicker.on('cancel.daterangepicker', function () {
            $(this).val('');
            $(this).trigger("change");
        });
    });

})();