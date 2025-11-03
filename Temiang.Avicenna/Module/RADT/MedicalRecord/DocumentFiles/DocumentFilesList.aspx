<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterList.Master" AutoEventWireup="true"
    CodeBehind="DocumentFilesList.aspx.cs" Inherits="Temiang.Avicenna.Module.RADT.MedicalRecord.DocumentFilesList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            function gotoAddUrl(dc) {
                var url = "DocumentFilesDetail.aspx?md=new&dc=" + dc;
                window.location.href = url;
            }
        </script>

    </telerik:RadCodeBlock>

    <script type="text/javascript" src="../../../../JavaScript/jquery.js"></script>

    <script type="text/javascript">
        $.download = function (url, data, method) {
            //url and data options required
            if (url && data) {
                //data can be string of parameters or array/object
                data = typeof data == 'string' ? data : $.param(data);
                //split params into form inputs
                var inputs = '';
                $.each(data.split('&'), function () {
                    var pair = this.split('=');
                    inputs += '<input type="hidden" name="' + pair[0] + '" value="' + pair[1] + '" />';
                });
                //send request
                $('<form action="' + url + '" method="' + (method || 'post') + '">' + inputs + '</form>').appendTo('body').submit().remove();
            };
        };

    </script>

    <telerik:RadGrid ID="grdList" runat="server" OnNeedDataSource="grdList_NeedDataSource">
        <MasterTableView DataKeyNames="DocumentFilesID">
            <Columns>
                <telerik:GridNumericColumn HeaderStyle-Width="10px" DataField="DocumentFilesID" HeaderText="Document ID"
                    UniqueName="DocumentFilesID" SortExpression="DocumentFilesID" HeaderStyle-HorizontalAlign="Right"
                    ItemStyle-HorizontalAlign="Right" Visible="false" />
                <telerik:GridBoundColumn HeaderStyle-Width="100px" DataField="DocumentNumber" HeaderText="Document No"
                    UniqueName="DocumentNumber" SortExpression="DocumentNumber" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn HeaderStyle-Width="240px" DataField="DocumentName" HeaderText="Document Name"
                    UniqueName="DocumentName" SortExpression="DocumentName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsQuality" HeaderText="Quality"
                    UniqueName="IsQuality" SortExpression="IsQuality" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsLegible" HeaderText="Legible"
                    UniqueName="IsLegible" SortExpression="IsLegible" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsSign" HeaderText="Signature"
                    UniqueName="IsSign" SortExpression="IsSign" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <telerik:GridCheckBoxColumn HeaderStyle-Width="70px" DataField="IsUsedForAnalysis"
                    HeaderText="Used For Analysis" UniqueName="IsUsedForAnalysis" SortExpression="IsUsedForAnalysis"
                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="DocumentType" HeaderText="Document Type"
                    UniqueName="DocumentType" SortExpression="DocumentType" HeaderStyle-HorizontalAlign="left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="AssessmentType" HeaderText="Assessment Type"
                    UniqueName="AssessmentType" SortExpression="AssessmentType" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="HaisMonitoring" HeaderText="Monitoring Type (Hais)"
                    UniqueName="HaisMonitoring" SortExpression="HaisMonitoring" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="QuestionFormName" HeaderText="Question Form (PHR)"
                    UniqueName="QuestionFormName" SortExpression="QuestionFormName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridBoundColumn DataField="ProgramName" HeaderText="Program Name"
                    UniqueName="ProgramName" SortExpression="ProgramName" HeaderStyle-HorizontalAlign="Left"
                    ItemStyle-HorizontalAlign="Left" />
                <telerik:GridTemplateColumn UniqueName="File" HeaderText="File">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FileTemplateName").ToString() == string.Empty ? string.Empty : string.Format("<a href=\"#\" onclick=\"$.download('DocumentFilesDownload.aspx','filename={0}'); return false;\"><img src=\"../../../../Images/Toolbar/download16.png\" border=\"0\" /></a>",
                        DataBinder.Eval(Container.DataItem, "FileTemplateName")) %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn HeaderStyle-Width="60px" DataField="IsActive" HeaderText="Active"
                    UniqueName="IsActive" SortExpression="IsActive" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
