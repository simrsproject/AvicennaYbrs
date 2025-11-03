<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MedHistAllergyCtl.ascx.cs" Inherits="Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl.MedHistAllergyCtl" %>
<%@ Import Namespace="Temiang.Avicenna.Common" %>

<table style="width: 100%; padding: 0 0 0 0;">
    <tr>     
        <td style="width: 50%;" valign="top">
            <table style="width: 100%;">             
                <tr>
                    <td valign="top" class="label">Allergies</td>
                    <td>
                        <telerik:RadGrid ID="grdPatientAllergy" runat="server" AutoGenerateColumns="False" EnableViewState="true" Width="100%"
                            OnNeedDataSource="grdPatientAllergy_NeedDataSource" Skin="" GridLines="None">
                            <MasterTableView DataKeyNames="ItemID" ShowHeader="False">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="StandardReferenceID" Display="False" UniqueName="StandardReferenceID" />
                                    <telerik:GridBoundColumn DataField="ItemID" Display="False" UniqueName="ItemID" />
                                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Allergen Name" SortExpression="ItemName"
                                        UniqueName="ItemName" HeaderStyle-Width="182px" />
                                    <telerik:GridTemplateColumn UniqueName="DescAndReaction" HeaderStyle-Width="300px">
                                        <ItemTemplate>
                                            <telerik:RadTextBox ID="txtAllergenDesc" runat="server" Width="100%" MaxLength="4000"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "DescAndReaction") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="False">
                                <Resizing AllowColumnResize="False" />
                            </ClientSettings>
                        </telerik:RadGrid>

                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
