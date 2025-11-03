<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master"
    AutoEventWireup="true" CodeBehind="DietLiquidPatientsDetail.aspx.cs" Inherits="Temiang.Avicenna.Module.Nutrient.Transaction.DietLiquidPatientsDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="RadCodeBlock1">

        <script language="javascript" type="text/javascript">
            function openWinRegistrationInfo() {
                var oWnd = $find("<%= winRegInfo.ClientID %>");
                var regNo = $find("<%= txtRegistrationNo.ClientID %>");
                var lblToBeUpdate = "<%= lblRegistrationInfo.ClientID %>";

                oWnd.setUrl('<%=Page.ResolveUrl("~/Module/RADT/RegistrationInfo/RegistrationInfoList.aspx?regNo=' + regNo.get_value() + '&lblRegistrationInfo=' + lblToBeUpdate + '")%>');
                oWnd.show();
            }

            function openDietLiquidItem(dietTime) {
                var rno = $find("<%= txtRegistrationNo.ClientID %>");
                var tno = $find("<%= txtTransactionNo.ClientID %>");
                var oWnd = $find("<%= winDietLiquid.ClientID %>");

                oWnd.setUrl('DietLiquidPatientsItemList.aspx?regno=' + rno.get_value() + '&transNo=' + tno.get_value() + "&time=" + dietTime);
                oWnd.set_title('Diet Liquid Item');
                oWnd.show();
                //oWnd.maximize();
                oWnd.add_pageLoad(onClientPageLoad);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winDietLiquid" Animation="None" Width="800px" Height="500px"
        runat="server" Behavior="Maximize,Close,Move" ShowContentDuringLoad="false" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadWindow ID="winRegInfo" Animation="None" Width="900px" Height="500px"
        runat="server" ShowContentDuringLoad="false" Behavior="Close" VisibleStatusbar="false"
        Modal="true" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRegistrationNo" runat="server" Text="Registration No / Medical No"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtRegistrationNo" runat="server" Width="197px" MaxLength="20"
                                ReadOnly="true" />
                            <telerik:RadTextBox ID="txtMedicalNo" runat="server" Width="100px" ReadOnly="true" />
                            <a href="javascript:void(0);" onclick="javascript:openWinRegistrationInfo();" class="noti_Container">
                                <asp:Label CssClass="noti_bubble" runat="server" ID="lblRegistrationInfo" AssociatedControlID="txtRegistrationNo"
                                    Text=""></asp:Label>&nbsp; </a>
                        </td>
                        <td width="20px">
                            <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" ErrorMessage="Registration No required."
                                ValidationGroup="entry" ControlToValidate="txtRegistrationNo" SetFocusOnError="True"
                                Width="100%">
                                <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                        </td>
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
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPatientName" runat="server" Width="245px" ReadOnly="true" />
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtGender" runat="server" Width="22px" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPlaceDOB" runat="server" Text="City / Date Of Birth"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPlaceDOB" runat="server" Width="300px" MaxLength="20"
                                ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        </td>
                        <td class="entry2Column">
                            <telerik:RadNumericTextBox ID="txtAgeInYear" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;Y&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeInMonth" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;M&nbsp;
                            <telerik:RadNumericTextBox ID="txtAgeInDay" runat="server" Width="30px" ReadOnly="true">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                            &nbsp;D
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; vertical-align: top;">
                <table width="100%">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblRoomID" runat="server" Text="Service Unit"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtUnitRoomBed" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblClassID" runat="server" Text="Class"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtClassID" runat="server" Width="100px" MaxLength="20" ReadOnly="true" />
                            <asp:Label ID="lblClassName" runat="server" CssClass="labeldescription" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblPhysician" runat="server" Text="Physician"></asp:Label>
                        </td>
                        <td class="entry">
                            <telerik:RadTextBox ID="txtPhysicianName" runat="server" Width="300px" ReadOnly="true" />
                        </td>
                        <td width="20">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%" cellpadding="0" cellspacing="1">
        <tr>
            <td>
                <fieldset>
                    <legend><asp:Label ID="Label3" runat="server" Text="DIET LIQUID PATIENT" Font-Bold="True" Font-Size="9"></asp:Label></legend>
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblTransactionNo" runat="server" Text="Transaction No"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtTransactionNo" runat="server" Width="300px" MaxLength="20"
                                                ReadOnly="true" />
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvTransactionNo" runat="server" ErrorMessage="Transaction No required."
                                                ValidationGroup="entry" ControlToValidate="txtTransactionNo" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblEffectiveStartDate" runat="server" Text="Effective Start Date / Time"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtEffectiveStartDate" runat="server" Width="100px">
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td>
                                                        <telerik:RadMaskedTextBox ID="txtEffectiveStartTime" runat="server" Mask="<00..23>:<00..59>"
                                                            PromptChar="_" RoundNumericRanges="false" Width="50px">
                                                        </telerik:RadMaskedTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="20px">
                                            <asp:RequiredFieldValidator ID="rfvEffectiveStartDate" runat="server" ErrorMessage="Effective Start Date required."
                                                ValidationGroup="entry" ControlToValidate="txtEffectiveStartDate" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image3" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvEffectiveStartTime" runat="server" ErrorMessage="Effective Start Time required."
                                                ValidationGroup="entry" ControlToValidate="txtEffectiveStartTime" SetFocusOnError="True"
                                                Width="100%">
                                                <asp:Image ID="Image4" runat="server" SkinID="rfvImage" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtNotes" runat="server" Width="300px" MaxLength="4000" Height="43px" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: top">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <b>- Information -</b>
                                        </td>
                                        <td>
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblDiagnose" runat="server" Text="Diagnose"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadTextBox ID="txtDiagnose" runat="server" Width="300px" MaxLength="250"
                                                ReadOnly="True" Height="30px" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblHeightWeight" runat="server" Text="Body Height / Weight"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtHeight" runat="server" Width="80px" MaxLength="10"
                                                MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                            Cm&nbsp;
                                            <telerik:RadNumericTextBox ID="txtWeight" runat="server" Width="80px" MaxLength="10"
                                                MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                            Kg
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBodyMassIndex" runat="server" Text="Body Mass Index"></asp:Label>
                                        </td>
                                        <td class="entry">
                                            <telerik:RadNumericTextBox ID="txtBodyMassIndex" runat="server" Width="80px" MaxLength="10"
                                                MinValue="0" NumberFormat-DecimalDigits="2" ReadOnly="True" />
                                        </td>
                                        <td width="20px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grdDietTime" runat="server" OnNeedDataSource="grdDietTime_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None" OnUpdateCommand="grdDietTime_UpdateCommand">
        <HeaderContextMenu>
        </HeaderContextMenu>
        <MasterTableView CommandItemDisplay="None" DataKeyNames="DietTime">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton">
                    <HeaderStyle Width="30px" />
                    <ItemStyle CssClass="MyImageButton" />
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="60px" DataField="DietTime" HeaderText="Time"
                    UniqueName="DietTime" SortExpression="DietTime" />
                <telerik:GridBoundColumn HeaderStyle-Width="250px" DataField="FoodName" HeaderText="Formula"
                    UniqueName="FoodName" SortExpression="FoodName" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="Measure" HeaderText="Measure"
                    UniqueName="Measure" SortExpression="Measure" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="AmountOfWater" HeaderText="Amount Of Water (ML)"
                    UniqueName="AmountOfWater" SortExpression="AmountOfWater" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}" />
                <telerik:GridBoundColumn HeaderStyle-Width="200px" DataField="Etc" HeaderText="Etc"
                    UniqueName="Etc" SortExpression="Etc" />
                <telerik:GridNumericColumn HeaderStyle-Width="100px" DataField="Total" HeaderText="Total (ML)"
                    UniqueName="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"
                    SortExpression="Notes" />
                <telerik:GridTemplateColumn UniqueName="process">
                    <ItemTemplate>
                        <%# (DataBinder.Eval(Container.DataItem, "FoodID").Equals(string.Empty) ? string.Empty :
                                                                  string.Format("<a href=\"#\" onclick=\"openDietLiquidItem('{0}'); return false;\"><img src=\"../../../../Images/Toolbar/details16.png\" border=\"0\" title=\"Diet Liquid Item\" /></a>",
                                                                                                                                                                 DataBinder.Eval(Container.DataItem, "DietTime")))%>
                    </ItemTemplate>
                    <HeaderStyle Width="35px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings UserControlName="DietLiquidPatientsTimeDetail.ascx" EditFormType="WebUserControl">
                <EditColumn UniqueName="EditCommandDietLiquidPatientsTime">
                </EditColumn>
            </EditFormSettings>
        </MasterTableView>
        <FilterMenu>
        </FilterMenu>
        <ClientSettings>
            <Resizing AllowColumnResize="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
