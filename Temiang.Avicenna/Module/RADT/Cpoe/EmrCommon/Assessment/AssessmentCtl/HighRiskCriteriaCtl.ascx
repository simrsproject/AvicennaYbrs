<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HighRiskCriteriaCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.HighRiskCriteriaCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<fieldset style="width: 49%;">
    <legend>Criteria for High Risk Patients Discharge Plan</legend>
    <table style="width: 100%">
        <tr>
            <td valign="top" class="label">Criteria for High Risk Patients Discharge Plan</td>
            <td>
                <telerik:RadGrid ID="grdHighRiskCriteria" Width="100%" Height="200px" runat="server" RenderMode="Lightweight" AutoGenerateColumns="False" EnableViewState="true"
                    GridLines="None" AllowMultiRowSelection="True" Skin=""
                    OnNeedDataSource="grdHighRiskCriteria_NeedDataSource" OnItemDataBound="grdHighRiskCriteria_ItemDataBound">
                    <MasterTableView DataKeyNames="ItemID" ShowHeader="False">
                        <Columns>
                            <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn1" HeaderStyle-Width="30px">
                            </telerik:GridClientSelectColumn>
                            <telerik:GridCheckBoxColumn DataField="IsSelected" UniqueName="IsSelected" HeaderText="" HeaderStyle-Width="30px" Display="False" />
                            <telerik:GridBoundColumn DataField="ItemName" UniqueName="ItemName" HeaderText="High Risk Criteria" />
                        </Columns>
                    </MasterTableView>
                    <FilterMenu>
                    </FilterMenu>
                    <ClientSettings EnableRowHoverStyle="False" AllowGroupExpandCollapse="False">
                        <Resizing AllowColumnResize="False" />
                        <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True"></Selecting>
                        <Scrolling UseStaticHeaders="True" AllowScroll="True"></Scrolling>
                    </ClientSettings>
                </telerik:RadGrid>

            </td>
        </tr>
        <tr>
            <td class="label"></td>
            <td >
                (If the question has 2 YES answers, then continue filling out the critical return planning form)
            </td>
        </tr>
    </table>
</fieldset>
