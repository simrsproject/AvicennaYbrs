<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="IncompleteMedicalRecordFileDialog.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.IncompleteMedicalRecordFileDialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="validationSummary" runat="server" ValidationGroup="entry" />
    <asp:CustomValidator ID="customValidator" ValidationGroup="entry" runat="server"></asp:CustomValidator>
    <asp:HiddenField runat="server" ID="hdnPatientID" />
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMedicalNo" runat="server" Text="Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtSalutation" runat="server" Width="28px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="245px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px"></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="22px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%" style="vertical-align: top">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationDate" runat="server" Text="Registration Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtRegistrationDate" runat="server" Width="100px" Enabled="false" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtRegistrationTime" runat="server" Width="50px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnit" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnit" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblParamedic" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtParamedic" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblDischargeDate" runat="server" Text="Discharge Date / Time"></asp:Label>
                        </td>
                        <td class="entry">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDischargeDate" runat="server" Width="100px" Enabled="false" />
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtDischargeTime" runat="server" Width="50px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="1" cellspacing="5">
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 50%; vertical-align: top">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="Label5" runat="server" Text="SUBMIT"></asp:Label>
                                </legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td width="100%" style="vertical-align: top">
                                            <table width="100%">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblSubmitDate" runat="server" Text="Submit Date"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadDatePicker ID="txtSubmitDate" runat="server" Width="100px" Enabled="false" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblSubmitBy" runat="server" Text="Submit By"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboSubmitByUserID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboSubmitByUserID_ItemDataBound"
                                                            OnItemsRequested="cboSubmitByUserID_ItemsRequested" Enabled="false">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Note : Show max 20 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblSubmitNotes" runat="server" Text="Submit Notes"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtSubmitNotes" runat="server" Width="300px" TextMode="MultiLine" MaxLength="1000" Height="50px" ReadOnly="true" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: 50%; vertical-align: top">
                            <fieldset>
                                <legend>
                                    <asp:Label ID="Label1" runat="server" Text="RETURN"></asp:Label>
                                </legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td width="100%" style="vertical-align: top">
                                            <table width="100%">
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblReturnDate" runat="server" Text="Return Date"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadDatePicker ID="txtReturnDate" runat="server" Width="100px" Enabled="false" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblReturnByUserID" runat="server" Text="Return By"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadComboBox ID="cboReturnByUserID" runat="server" Width="300px" EnableLoadOnDemand="true"
                                                            MarkFirstMatch="true" HighlightTemplatedItems="true" OnItemDataBound="cboReturnByUserID_ItemDataBound"
                                                            OnItemsRequested="cboReturnByUserID_ItemsRequested" Enabled="false">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Note : Show max 20 items
                                                            </FooterTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        <asp:Label ID="lblReturnNotes" runat="server" Text="Notes"></asp:Label>
                                                    </td>
                                                    <td class="entry">
                                                        <telerik:RadTextBox ID="txtReturnNotes" runat="server" Width="300px" TextMode="MultiLine" MaxLength="1000" Height="50px" />
                                                    </td>
                                                    <td width="20px"></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <br />
                <telerik:RadGrid ID="grdItem" runat="server" OnNeedDataSource="grdItem_NeedDataSource" AutoGenerateColumns="False" GridLines="None">
                    <MasterTableView DataKeyNames="DocumentFilesID" CommandItemDisplay="None" ShowHeader="True">
                        <Columns>
                            <telerik:GridBoundColumn HeaderStyle-Width="50px" DataField="DocumentFilesID" HeaderText="ID"
                                UniqueName="DocumentFilesID" SortExpression="DocumentFilesID" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" Visible="false" />
                            <telerik:GridBoundColumn HeaderStyle-Width="150px" DataField="DocumentNumber" HeaderText="Document Number"
                                UniqueName="DocumentNumber" SortExpression="DocumentNumber" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="DocumentName" HeaderText="Document Name"
                                UniqueName="DocumentName" SortExpression="DocumentName" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn HeaderStyle-Width="500px" DataField="Notes" HeaderText="Notes To Unit"
                                UniqueName="Notes" SortExpression="Notes" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridTemplateColumn />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="false">
                        <Resizing AllowColumnResize="True" />
                        <Selecting AllowRowSelect="False" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
