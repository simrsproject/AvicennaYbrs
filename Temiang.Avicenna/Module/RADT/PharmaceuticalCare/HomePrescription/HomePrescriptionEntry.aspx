<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterDialogEntry.Master" AutoEventWireup="true"
    CodeBehind="HomePrescriptionEntry.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.PharmaceuticalCare.HomePrescriptionEntry" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CustomControl/RegistrationInfoCtl.ascx" TagPrefix="uc1" TagName="RegistrationInfoCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="<%=Helper.UrlRoot()%>/App_Themes/Default/SmallSwitch.css">
    <script type="text/javascript">
        function UpdateAlertIconOnParent(objectName, iCount) {
            if (objectName == null || objectName == undefined || objectName == 'none') {
                // do nothing
            } else {
                var obj = GetRadWindow().BrowserWindow.document.getElementById(objectName);
                obj.innerHTML = iCount;
                if (iCount > 0) {
                    // set bubble visible true
                    obj.style.visibility = 'visible';
                } else {
                    // set bubble visible false
                    obj.style.visibility = 'hidden';
                }
            }
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
    </script>

    <uc1:RegistrationInfoCtl runat="server" ID="RegistrationInfoCtl" />

    <table width="500px">
        <tr>
            <td class="label">Education Date 
            </td>
            <td class="entry">
                <telerik:RadDateTimePicker ID="txtEduDateTime" runat="server" Width="170px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Education Time required."
                    ValidationGroup="entry" ControlToValidate="txtEduDateTime" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">Start 
            </td>
            <td class="entry">
                <telerik:RadTimePicker ID="txtStartDateTime" runat="server" Width="100px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Start Time required."
                    ValidationGroup="entry" ControlToValidate="txtEduDateTime" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">Finish 
            </td>
            <td class="entry">
                <telerik:RadTimePicker ID="txtFinishDateTime" runat="server" Width="100px" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Start Time required."
                    ValidationGroup="entry" ControlToValidate="txtEduDateTime" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="label">Education Recipient
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsRecipientAsPatient" runat="server" Text="Patient" /> &nbsp;
                <telerik:RadTextBox ID="txtRecipientName" runat="server" Width="200px" />
            </td>
            <td>
            </td>
        </tr>
                <tr>
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsNgt" runat="server" Text="NGT" /> &nbsp;
                <asp:CheckBox ID="chkIsOralHygiene" runat="server" Text="Oral Hygiene" /> 
            </td>
            <td>

            </td>
        </tr>
    </table>

    <telerik:RadGrid ID="grdEntry" runat="server" OnNeedDataSource="grdEntry_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" AllowPaging="false">
        <MasterTableView DataKeyNames="MedicationReceiveNo">
            <ColumnGroups>
                <telerik:GridColumnGroup Name="cm" HeaderText="Consume Method" HeaderStyle-HorizontalAlign="Center"></telerik:GridColumnGroup>
            </ColumnGroups>
            <Columns>
                <telerik:GridTemplateColumn UniqueName="TemplateItemName3" HeaderStyle-Width="350px" HeaderText="Drug Name">
                    <ItemTemplate>
                        <strong><%# DataBinder.Eval(Container.DataItem, "ItemDescription")%></strong><br />
                        <%# DataBinder.Eval(Container.DataItem, "SRConsumeMethodName")%>&nbsp;@<%# DataBinder.Eval(Container.DataItem, "ConsumeQty")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRConsumeUnit")%><br />
                        <%# string.Format("Bal: {0} {1}", DataBinder.Eval(Container.DataItem, "BalanceQty"),DataBinder.Eval(Container.DataItem, "SRConsumeUnit"))%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <%--<telerik:GridCheckBoxColumn DataField="IsBroughtHome" DataType="System.Boolean" HeaderText="HP" UniqueName="IsBroughtHome" HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="Center"></telerik:GridCheckBoxColumn>--%>
<%--                <telerik:GridTemplateColumn HeaderText="Select" UniqueName="Select" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <center>
                            <label class="switch">
                                <%# string.Format("<input id=\"chkOnOff\" type=\"checkbox\" name=\"chkOnOff_{2}\" {0} {1}/>",DataBinder.Eval(Container.DataItem, "IsSelect").Equals(true)?"checked=\"checked\"":string.Empty, DataModeCurrent == AppEnum.DataMode.Read ?"disabled=\"disabled\"":string.Empty, DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"))%>
                                <span class="slider round"></span>
                            </label>
                        </center>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>

                <telerik:GridTemplateColumn HeaderText="Morning" UniqueName="Morning" HeaderStyle-Width="150px" ColumnGroupName="cm" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadTextBox
                            ID="txtMorning" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Morning")%>' ReadOnly="<%# DataModeCurrent == AppEnum.DataMode.Read%>"
                            Width="100%">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Noon" UniqueName="Noon" HeaderStyle-Width="150px" ColumnGroupName="cm" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadTextBox
                            ID="txtNoon" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Noon")%>' ReadOnly="<%# DataModeCurrent == AppEnum.DataMode.Read%>"
                            Width="100%">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Afternoon" UniqueName="Afternoon" HeaderStyle-Width="150px" ColumnGroupName="cm" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadTextBox
                            ID="txtAfternoon" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Afternoon")%>' ReadOnly="<%# DataModeCurrent == AppEnum.DataMode.Read%>"
                            Width="100%">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Night" UniqueName="Night" HeaderStyle-Width="150px" ColumnGroupName="cm" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadTextBox
                            ID="txtNight" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Night")%>' ReadOnly="<%# DataModeCurrent == AppEnum.DataMode.Read%>"
                            Width="100%">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Note" UniqueName="Note" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <telerik:RadTextBox
                            ID="txtNote" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Note")%>' ReadOnly="<%# DataModeCurrent == AppEnum.DataMode.Read%>"
                            Width="100%" TextMode="MultiLine">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Indication" UniqueName="Indication" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <telerik:RadTextBox
                            ID="txtIndication" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Indication")%>' ReadOnly="<%# DataModeCurrent == AppEnum.DataMode.Read%>"
                            Width="100%" TextMode="MultiLine">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
        </ClientSettings>
    </telerik:RadGrid>

</asp:Content>
