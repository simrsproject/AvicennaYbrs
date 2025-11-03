<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true"
    CodeBehind="MedicationReceiveOpt.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.MedicationReceiveOpt" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>Medication Receive</legend>

        <telerik:RadToolBar ID="tbarMedication" runat="server" Width="100%" EnableEmbeddedScripts="false" Orientation="Vertical"
            OnButtonClick="tbarMedication_OnButtonClick">
            <CollapseAnimation Duration="200" Type="OutQuint" />
            <Items>
                <telerik:RadToolBarButton runat="server" Text="Drug Maintenance & Review" Value="maintenance" ImageUrl="~/Images/Toolbar/views16.png"
                    HoveredImageUrl="~/Images/Toolbar/views16_h.png" DisabledImageUrl="~/Images/Toolbar/views16_d.png" />
                <telerik:RadToolBarButton runat="server" Text="Drug Acceptance from Internal Prescription" Value="prescription" ImageUrl="~/Images/Toolbar/new16.png"
                    HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
                <telerik:RadToolBarButton runat="server" Text="Drug Acceptance from Patient" Value="patient" ImageUrl="~/Images/Toolbar/new16.png"
                    HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
                <telerik:RadToolBarButton runat="server" Text="Drug Acceptance from Service Unit Tansaction" Value="serviceunit" ImageUrl="~/Images/Toolbar/new16.png"
                    HoveredImageUrl="~/Images/Toolbar/new16_h.png" DisabledImageUrl="~/Images/Toolbar/new16_d.png" />
            </Items>
        </telerik:RadToolBar>
    </fieldset>
        <fieldset>
        <legend>Reconciliation</legend>

        <telerik:RadToolBar ID="tbarReconciliation" runat="server" Width="100%" EnableEmbeddedScripts="false" Orientation="Vertical"
            OnButtonClick="tbarMedication_OnButtonClick">
            <CollapseAnimation Duration="200" Type="OutQuint" />
            <Items>
                <telerik:RadToolBarButton runat="server" Text="Admission Drug Reconciliation" Value="adm_reconciliation" ImageUrl="~/Images/Toolbar/post16.png"
                                          HoveredImageUrl="~/Images/Toolbar/post16_h.png" DisabledImageUrl="~/Images/Toolbar/post16_d.png" />
                <telerik:RadToolBarButton runat="server" Text="Transfer Drug Reconciliation" Value="trf_reconciliation" ImageUrl="~/Images/Toolbar/post16.png"
                                          HoveredImageUrl="~/Images/Toolbar/post16_h.png" DisabledImageUrl="~/Images/Toolbar/post16_d.png" />
                <telerik:RadToolBarButton runat="server" Text="Discharge Drug Reconciliation" Value="dcg_reconciliation" ImageUrl="~/Images/Toolbar/post16.png"
                                          HoveredImageUrl="~/Images/Toolbar/post16_h.png" DisabledImageUrl="~/Images/Toolbar/post16_d.png" />

            </Items>
        </telerik:RadToolBar>
    </fieldset>
    <fieldset>
        <legend>UDD</legend>

        <telerik:RadToolBar ID="tbarMedicationSetup" runat="server" Width="100%" EnableEmbeddedScripts="false" Orientation="Vertical"
                            OnButtonClick="tbartbarMedicationSetup_OnButtonClick">
            <CollapseAnimation Duration="200" Type="OutQuint" />
            <Items>
                <telerik:RadToolBarButton runat="server" Text="Medication Setup Status" Value="udd_setup" ImageUrl="~/Images/Toolbar/post16.png"
                                          HoveredImageUrl="~/Images/Toolbar/post16_h.png" DisabledImageUrl="~/Images/Toolbar/post16_d.png" />
                <telerik:RadToolBarButton runat="server" Text="Medication Verification Status" Value="udd_verification" ImageUrl="~/Images/Toolbar/post16.png"
                                          HoveredImageUrl="~/Images/Toolbar/post16_h.png" DisabledImageUrl="~/Images/Toolbar/post16_d.png" />
                <telerik:RadToolBarButton runat="server" Text="Medication Realization Status" Value="udd_realization" ImageUrl="~/Images/Toolbar/post16.png"
                                          HoveredImageUrl="~/Images/Toolbar/post16_h.png" DisabledImageUrl="~/Images/Toolbar/post16_d.png" />

            </Items>
        </telerik:RadToolBar>
    </fieldset>
</asp:Content>
