<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" AutoEventWireup="true" CodeBehind="InosInfectionMonitoringGroupDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.InPatient.InosInfectionMonitoringGroupDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblServiceUnitID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtServiceUnitID" runat="server" Width="100px" MaxLength="20"
                                ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblServiceUnitName" runat="server"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Room"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRoomID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                            &nbsp;
                            <asp:Label ID="lblRoomName" runat="server"></asp:Label>
                        </td>
                        <td width="20"></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblMonitoringDate" runat="server" Text="Monitoring Date"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadDatePicker ID="txtMonitoringDate" runat="server" Width="100px">
                            </telerik:RadDatePicker>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvMonitoringDate" runat="server" ErrorMessage="Monitoring Date required."
                                ValidationGroup="entry" ControlToValidate="txtMonitoringDate" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image8" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource" Height="680px"
        AutoGenerateColumns="False" GridLines="None" AllowSorting="true">
        <MasterTableView CommandItemDisplay="None" DataKeyNames="RegistrationNo" ShowHeader="True" HierarchyDefaultExpanded="true">
            <NestedViewTemplate>
                <div style="padding-left: 20px; width: 98%">
                    <table style="width: 100%" cellpadding="0" cellspacing="1">
                        <tr>
                            <td>
                                <fieldset>
                                    <legend><b>HAIs Monitoring</b>&nbsp;&nbsp;<asp:Label runat="server" ID="lblVoid" Text='<%#Eval("CaptionVoid")%>' ForeColor="Red"></asp:Label></legend>
                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width: 50%; vertical-align: top">
                                                <table width="100%">
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblTreatment" runat="server" Text="Treatment"></asp:Label>
                                                        </td>
                                                        <td class="entry" colspan="3">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="width: 150px">
                                                                        <asp:CheckBox ID="chkIsMechanicalVentilator" runat="server" Text="Mechanical Ventilator" Checked='<%#Eval("IsMechanicalVentilator")%>' Enabled='<%#Eval("IsEnabled")%>' />
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                    <td style="width: 150px">
                                                                        <asp:CheckBox ID="chkIsUrineCatheter" runat="server" Text="Urine Catheter" Checked='<%#Eval("IsUrineCatheter")%>' Enabled='<%#Eval("IsEnabled")%>' />
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                    <td style="width: 150px">
                                                                        <asp:CheckBox ID="chkIsCentralVeinLine" runat="server" Text="Central Vein Line" Checked='<%#Eval("IsCentralVeinLine")%>' Enabled='<%#Eval("IsEnabled")%>' />

                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkIsTotalCare" runat="server" Text="Total Care" Checked='<%#Eval("IsTotalCare")%>' Enabled='<%#Eval("IsEnabled")%>' />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkIsInpatient" runat="server" Text="Inpatient" Checked='<%#Eval("IsInpatient")%>' Enabled='<%#Eval("IsEnabled")%>' />
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkIsSurgery" runat="server" Text="Surgery" Checked='<%#Eval("IsSurgery")%>' Enabled='<%#Eval("IsEnabled")%>' />
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkIsIntraVeinLine" runat="server" Text="Intra Vein Line" Checked='<%#Eval("IsIntraVeinLine")%>' Enabled='<%#Eval("IsEnabled")%>' />
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkIsAntibioticDrugs" runat="server" Text="Antibiotic Drugs" Checked='<%#Eval("IsAntibioticDrugs")%>' Enabled='<%#Eval("IsEnabled")%>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table width="100%">
                                                                <hr />
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblIncidenceOfHAIs" runat="server" Text="Incidence of HAIs"></asp:Label>
                                                        </td>
                                                        <td class="entry" colspan="3">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="width: 150px">
                                                                        <asp:CheckBox ID="chkIsVAP" runat="server" Text="VAP" Checked='<%#Eval("IsVAP")%>' Enabled='<%#Eval("IsEnabled")%>' />
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                    <td style="width: 150px">
                                                                        <asp:CheckBox ID="chkIsISK" runat="server" Text="ISK" Checked='<%#Eval("IsISK")%>' Enabled='<%#Eval("IsEnabled")%>' />
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkIsIADP" runat="server" Text="IADP" Checked='<%#Eval("IsIADP")%>' Enabled='<%#Eval("IsEnabled")%>' />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkIsHAP" runat="server" Text="HAP" Checked='<%#Eval("IsHAP")%>' Enabled='<%#Eval("IsEnabled")%>' />
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkIsILO" runat="server" Text="ILO" Checked='<%#Eval("IsILO")%>' Enabled='<%#Eval("IsEnabled")%>' />
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                    <td></td>
                                                                </tr>

                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table width="100%">
                                                                <hr />
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label">
                                                            <asp:Label ID="lblIncidenceOfOtherInfections" runat="server" Text="Incidence of Other Infections"></asp:Label>
                                                        </td>
                                                        <td class="entry" colspan="3">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="width: 150px">
                                                                        <asp:CheckBox ID="chkIsPhlebitis" runat="server" Text="Phlebitis" Checked='<%#Eval("IsPhlebitis")%>' Enabled='<%#Eval("IsEnabled")%>' />
                                                                    </td>
                                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkIsDecubitus" runat="server" Text="Decubitus" Checked='<%#Eval("IsDecubitus")%>' Enabled='<%#Eval("IsEnabled")%>' /></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr style="display:none">
                                                        <td class="label"></td>
                                                        <td class="entry" colspan="3">
                                                            <asp:CheckBox ID="chkIsVoid" runat="server" Text="Void" Checked='<%#Eval("IsVoid")%>' Enabled="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 50%; vertical-align: top">
                                                <table width="100%">
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </div>
            </NestedViewTemplate>
            <Columns>
                <telerik:GridBoundColumn DataField="RegistrationDate" HeaderText="Reg. Date" UniqueName="RegistrationDate"
                    SortExpression="RegistrationDate" DataType="System.DateTime" DataFormatString="{0:dd-MMM-yyyy}">
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridNumericColumn HeaderStyle-Width="60px" DataField="Los" HeaderText="LOS"
                    UniqueName="Los" SortExpression="Los" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" />
                <telerik:GridBoundColumn DataField="RegistrationNo" HeaderText="Registration No" UniqueName="RegistrationNo"
                    SortExpression="RegistrationNo">
                    <HeaderStyle HorizontalAlign="Center" Width="145px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MedicalNo" HeaderText="Medical No" UniqueName="MedicalNo"
                    SortExpression="MedicalNo">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="250px" DataField="PatientName" HeaderText="Patient Name" UniqueName="PatientName"
                    SortExpression="PatientName">
                    <ItemTemplate>
                        <%# string.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "SalutationName"), DataBinder.Eval(Container.DataItem, "PatientName"))%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" UniqueName="Sex" SortExpression="Sex">
                    <HeaderStyle HorizontalAlign="Center" Width="55px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="BedID" HeaderText="Bed No" UniqueName="BedID"
                    SortExpression="BedID">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ParamedicName" HeaderText="Physician" UniqueName="ParamedicName"
                    SortExpression="ParamedicName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="GuarantorName" HeaderText="Guarantor" UniqueName="GuarantorName"
                    SortExpression="GuarantorName">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
