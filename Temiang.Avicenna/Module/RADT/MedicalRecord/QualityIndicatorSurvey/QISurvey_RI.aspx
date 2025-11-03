<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDetail.Master" 
    AutoEventWireup="true" CodeBehind="QISurvey_RI.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.QISurvey_RI" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript" language="javascript">
            function onProcess() {
                __doPostBack("<%= grdQISurvey_RI.UniqueID %>", "process");
            }
            function onPrint() {
                __doPostBack("<%= grdQISurvey_RI.UniqueID %>", "print");
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindow ID="winPrint" Animation="None" Width="1000px" Height="500px" runat="server" 
        ShowContentDuringLoad="false" Behaviors="Maximize,Close" VisibleStatusbar="false"
        Modal="true">
    </telerik:RadWindow>
    <telerik:RadAjaxLoadingPanel ID="ajxLoadingPanel" runat="server" Transparency="30">
        <img alt="Loading..." src='<%= RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Ajax.loading.gif") %>'
            style="border: 0px; margin-top: 75px;" />
    </telerik:RadAjaxLoadingPanel>
    <table width="100%">
        <tr>
            <td class="label">
                <asp:Label ID="lblStandardReferenceID" runat="server" Text="Master Quality Indicator"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadTextBox ID="txtStandardReferenceID" runat="server" Width="100px" ReadOnly="true" />
                <telerik:RadTextBox ID="txtStandardReferenceName" runat="server" Width="200px" ReadOnly="true" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvStandardReferenceID" runat="server" ErrorMessage="Quality Indicator ID required."
                    ValidationGroup="entry" ControlToValidate="txtStandardReferenceID" SetFocusOnError="true"
                    Width="100%">
                    <asp:Image ID="Image1" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <%--<td>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <asp:Panel runat="server" ID="pnlPrint">
                        <tr>
                            <td>
                                <asp:LinkButton ID="lbPrint" runat="server" OnClientClick="javascript:onPrint();return false;">
                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../Images/Toolbar/print16.png" />
                                    &nbsp;<asp:Label runat="server" ID="lblPrint" Text="Print Report" Font-Bold="True"></asp:Label>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </td>--%>
        </tr>
        <tr style="display:none">
            <td class="label">
                <asp:Label ID="lblSurveyID" runat="server" Text="Quality Indicator ID"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadNumericTextBox ID="txtSurveyID" runat="server" Width="300px" MaxLength="10" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
             <td class="label">
                 <asp:Label ID="Label6" runat="server" Text="Service Unit"></asp:Label>
               </td>
                  <td class="entry">
                    <telerik:RadComboBox ID="cboServiceUnitID" runat="server" Width="300px" AllowCustomText="true"
                         Filter="Contains" />
                   </td>
                   <td width="20">
                     <asp:RequiredFieldValidator ID="rfvServiceUnit" runat="server" ErrorMessage="Service Unit required."
                          ValidationGroup="entry" ControlToValidate="cboServiceUnitID" SetFocusOnError="True"
                           Width="100%">
                     <asp:Image ID="Image6" runat="server" SkinID="rfvImage" />
                      </asp:RequiredFieldValidator>
                  </td>
                  <td>
                  </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblPeriodDate" runat="server" Text="Period Date (@p_date)"></asp:Label>
            </td>
            <td class="entry">
                <telerik:RadDatePicker ID="txtPeriodDate" runat="server" Width="100px" />
            </td>
            <td width="20">
                <asp:RequiredFieldValidator ID="rfvPeriodDate" runat="server" ErrorMessage="Period Date required."
                    ValidationGroup="entry" ControlToValidate="txtPeriodDate" SetFocusOnError="True"
                    Width="100%">
                    <asp:Image ID="Image2" runat="server" SkinID="rfvImage" />
                </asp:RequiredFieldValidator>
            </td>
            <td></td>
        </tr>
             <tr>
                 <td class="label">
                      <asp:Label ID="lblCreateByUserID" runat="server" Text="Create By User ID"></asp:Label>
                   </td>
                    <td class="entry">
                       <telerik:RadTextBox ID="txtCreateByUserID" runat="server" Width="300px" MaxLength="100"
                          ReadOnly="True" />
                   </td>
                   <td width="20">
                   </td>
                   <td>
                   </td>
           </tr>
        <tr style="display: none">
            <td class="label">
            </td>
            <td class="entry">
                <asp:CheckBox ID="chkIsApproved"  runat="server" Text="Approved" Enabled="false" />
                <asp:CheckBox ID="chkIsVoid"  runat="server" Text="Void" Enabled="false" />
            </td>
            <td width="20">
            </td>
            <td></td>
        </tr>
    </table>
    <telerik:RadAjaxPanel ID="ajxPanel" runat="server" Width="100%" LoadingPanelID="ajxLoadingPanel">
        <telerik:RadGrid ID="grdQISurvey_RI" runat="server" OnNeedDataSource="grdQISurvey_RI_NeedDataSource"
            OnPreRender="grdQISurvey_RI_PreRender"
            AutoGenerateColumns="false" GridLines="None">
            <HeaderContextMenu ViewStateMode="Disabled">
            </HeaderContextMenu>
            <MasterTableView CommandItemDisplay="None" DataKeyNames="ItemID">
                <CommandItemTemplate>
                </CommandItemTemplate>
                <commanditemtemplate>
                    &nbsp;&nbsp;&nbsp;
                    <asp:linkbutton id="lbpicklist" runat="server" onclientclick="javascript:onProcess();return false;">
                        <img style="border: 0px; vertical-align: middle;" alt="" src="../../../../images/toolbar/process16.png" />
                        &nbsp;<asp:label runat="server" id="lblpiclist" text="Process Data"></asp:label>
                    </asp:linkbutton>
                </commanditemtemplate>
                <CommandItemStyle Height="29px"/>
                <Columns>
                    <telerik:GridBoundColumn DataField="LineNumber" HeaderText="No" UniqueName="LineNumber"
                        SortExpression="LineNumber">
                        <HeaderStyle HorizontalAlign="Center" Width="35px" Font-Bold="true"/>
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="ItemName" HeaderText="Variabel Group" UniqueName="ItemName"  HeaderStyle-Font-Bold="true"
                        SortExpression="ItemName">
                    </telerik:GridBoundColumn>

                    <telerik:GridTemplateColumn HeaderText="Variabel" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true">
                        <ItemTemplate>
                            <%# Eval("Note") %> <br />
                            <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblErrorMessage2" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn HeaderStyle-Width="65px" HeaderText="Num" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"  UniqueName="Num">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtNumerator" runat="server" Width="55px" DbValue='<%#Eval("Numerator")%>'
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="215px" HeaderText="Input Query Numerator" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Visible="false">
                        <ItemTemplate>
                            <telerik:RadTextBox ID="txtInputQueryNumer" runat="server" Width="215px" Text='<%#Eval("InputQueryNumer")%>'/>
                            <%--<asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>--%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="65px" HeaderText="Denum" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" UniqueName="Denum">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="txtValueDemun" runat="server" Width="55px" DbValue='<%#Eval("Denumerator")%>' 
                                NumberFormat-DecimalDigits="0" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderStyle-Width="215px" HeaderText="Input Query Denumerator" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Visible="false">
                        <ItemTemplate>
                            <telerik:RadTextBox ID="txtInputQDenum" runat="server" Width="215px" Text='<%#Eval("InputQueryDenum")%>' />
                            <%--<asp:Label ID="lblErrorMessage2" runat="server" Text="" ForeColor="Red"></asp:Label>--%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <FilterMenu>
            </FilterMenu>
            <ClientSettings EnableRowHoverStyle="True">
                <Resizing AllowColumnResize="True" />
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
</asp:Content>
