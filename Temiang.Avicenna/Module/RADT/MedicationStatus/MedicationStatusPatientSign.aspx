<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterDialog.Master" AutoEventWireup="true" CodeBehind="MedicationStatusPatientSign.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicationStatus.MedicationStatusPatientSign" %>

<%@ Import Namespace="Temiang.Avicenna.Common" %>
<%@ Register Src="~/CustomControl/RegistrationInfoCtl.ascx" TagPrefix="uc1" TagName="RegistrationInfoCtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="codeBlock">
        <script type="text/javascript" language="javascript">;

            function openPatientSign() {
                <%=IsModeViewHist? "return":string.Empty%>
                var imgId = '<%=imgPatientSign.ClientID %>';
                var txtId = '<%=hdnPatientSign.ClientID %>';
                var url = '<%=Helper.UrlRoot()%>/CustomControl/PHR/InputControl/Signature/SignatureEdit.html?mod=edt&imgId=' + imgId + '&txtId=' + txtId;
                var oWnd = $find("<%= winSign.ClientID %>");
                oWnd.setUrl(url);
                oWnd.show();
            }


            function winSign_ClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg != null) {
                    document.getElementById(arg.txtId).value = arg.image;
                    var img = document.getElementById(arg.imgId);
                    img.setAttribute('src', "data:image/Png;base64," + arg.image);
                }
            }

        </script>
    </telerik:RadCodeBlock>

    <telerik:RadWindow runat="server" Animation="None" Width="800px" Height="620px" Behavior="Close"
        ShowContentDuringLoad="False" VisibleStatusbar="false" Modal="true" OnClientClose="winSign_ClientClose"
        ID="winSign" />
    <table>
        <tr>
            <td>
                <uc1:RegistrationInfoCtl runat="server" ID="regInfoCtl" IsShowVitalSign="false" IsShowDianosis="false" IsShowPatientAllergy="false" IsShowPhysicianTeam="false" />
            </td>
            <td>
                <fieldset style="width: 128px">
                    <legend>Family/Patient Signature</legend>
                    <a onclick="javascript:openPatientSign();return false;">
                        <telerik:RadBinaryImage ID="imgPatientSign" runat="server"
                            Width="300px" Height="125px" ResizeMode="Fit"></telerik:RadBinaryImage>
                    </a>
                    <br />
                    <asp:Label runat="server" id="lblSignDate" Text="" Width="294px" BorderStyle="Solid" style="text-align:center; border-color: gray;"></asp:Label>
                    <br />
                    <asp:Button runat="server" ID="btnPatientSign" Text="Sign" Width="296px" OnClientClick="javascript:openPatientSign();return false;" />
                    <div>
                        <asp:HiddenField runat="server" ID="hdnPatientSign" />
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>

    <telerik:RadGrid ID="grdMedication" runat="server" OnNeedDataSource="grdMedication_NeedDataSource"
        AutoGenerateColumns="False" GridLines="None"
        AllowPaging="false">
        <MasterTableView DataKeyNames="MedicationReceiveNo,SequenceNo">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="" UniqueName="Select" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <center>
                            <label class="switch">
                                <%# string.Format("<input id=\"chkOnOff\" type=\"checkbox\" name=\"chkOnOff_{2}_{3}\" {0} {1}/>",DataBinder.Eval(Container.DataItem, "IsSelect").Equals(true)?"checked=\"checked\"":string.Empty, string.Empty, DataBinder.Eval(Container.DataItem, "MedicationReceiveNo"),DataBinder.Eval(Container.DataItem, "SequenceNo"))%>
                                <span class="slider round"></span>
                            </label>
                        </center>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn UniqueName="ItemDescription" HeaderStyle-Width="300px" HeaderText="Drug Item">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ItemDescription")%><br />
                        <%# DataBinder.Eval(Container.DataItem, "SRConsumeMethodName")%>&nbsp;@<%# DataBinder.Eval(Container.DataItem, "ConsumeQty")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "SRConsumeUnit")%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridDateTimeColumn DataField="ScheduleDateTime" HeaderText="Schedule" UniqueName="ScheduleDateTime" HeaderStyle-Width="110px" />
                <telerik:GridDateTimeColumn DataField="RealizedDateTime" HeaderText="Realized" UniqueName="RealizedDateTime" HeaderStyle-Width="110px" />
                <telerik:GridNumericColumn DataField="Qty" UniqueName="Qty" HeaderText="Qty" DecimalDigits="2" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
                <telerik:GridCheckBoxColumn DataField="IsNotConsume" UniqueName="IsNotConsume" HeaderText="Not Cons" HeaderStyle-Width="70px" />
                <telerik:GridCheckBoxColumn DataField="IsReSchedule" UniqueName="IsReSchedule" HeaderText="Resch" HeaderStyle-Width="70px" />
                <telerik:GridBoundColumn DataField="RealizedByUserName" UniqueName="RealizedByUserName" HeaderText="Realized By" HeaderStyle-Width="120px" />
                <telerik:GridBoundColumn DataField="Note" UniqueName="Note" HeaderText="Note" />
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="False">
            <Selecting AllowRowSelect="False" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
