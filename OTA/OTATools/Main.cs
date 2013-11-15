using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using OTATools.Resources;

namespace OTATools
{
    
    public partial class Main : Form
    {
        private OTAManager ota;
        private MessageHandler handler;
        private string modelId;
        private string updateId;
        private Dictionary<string, UpdateFileInfo> updates;
        public Main()
        {
            InitializeComponent();
            handler = new MessageHandler(this.HandleMessge);
            ota = new OTAManager(handler, this);
            listViewDevices.ListViewItemSorter = new ListViewColumnSorter();
            (listViewDevices.ListViewItemSorter as ListViewColumnSorter).SortColumn = 4;
            (listViewDevices.ListViewItemSorter as ListViewColumnSorter).Order = listViewDevices.Sorting;
            listViewDevices.ColumnClick += new ColumnClickEventHandler(ListViewHelper.ListView_ColumnClick);
            listViewFirmwares.ListViewItemSorter = new ListViewColumnSorter();
            (listViewFirmwares.ListViewItemSorter as ListViewColumnSorter).SortColumn = 5;
            (listViewFirmwares.ListViewItemSorter as ListViewColumnSorter).Order = listViewFirmwares.Sorting;
            listViewFirmwares.ColumnClick += new ColumnClickEventHandler(ListViewHelper.ListView_ColumnClick);
            this.RefreshDeviceList();
        }

        public void HandleMessge(int msgId, object obj)
        {
            switch (msgId)
            {
                case Messages.MSG_SAVE_DEVICES_START:
                    panelProgress.Visible = true;
                    labelProgress.Text = Resources.Strings.committing;
                    break;

                case Messages.MSG_SAVE_DEVICES_FAIL:
                    panelProgress.Visible = false;
                    break;

                case Messages.MSG_SAVE_DEVICES_SUCCESS:
                    panelProgress.Visible = false;
                    RefreshDeviceList();
                    break;
                case Messages.MSG_GET_DEVICES_START:
                    panelProgress.Visible = true;
                    labelProgress.Text = Resources.Strings.loading;
                    listViewDevices.Items.Clear();
                    break;
                case Messages.MSG_GET_DEVICES_SUCCESS:
                    panelProgress.Visible = false;
                    RefreshDeviceList();
                    break;

                case Messages.MSG_GET_DEVICES_FAIL:
                    panelProgress.Visible = false;
                    break;

                case Messages.MSG_SAVE_UPDATE_FILE_LIST_START:
                    panelProgress2.Visible = true;
                    labelProgress2.Text = Resources.Strings.committing;
                    break;

                case Messages.MSG_SAVE_UPDATE_FILE_LIST_SUCCESS:
                    panelProgress2.Visible = false;
                    RefreshUpdateList();
                    break;

                case Messages.MSG_SAVE_UPDATE_FILE_LIST_FAIL:
                    panelProgress2.Visible = false;
                    break;

                case Messages.MSG_GET_UPDATE_FILE_LIST_START:
                    panelProgress2.Visible = true;
                    labelProgress2.Text = Resources.Strings.loading;
                    listViewFirmwares.Items.Clear();
                    tabControl1.SelectedIndex = 1;
                    break;
                case Messages.MSG_GET_UPDATE_FILE_LIST_SUCCESS:
                    panelProgress2.Visible = false;
                    updates = (Dictionary<string, UpdateFileInfo>)obj;
                    RefreshUpdateList();
                    break;
                case Messages.MSG_GET_UPDATE_FILE_LIST_FAIL:
                    panelProgress2.Visible = false;

                    break;

                case Messages.MSG_DELETE_UPDATE_FILE_START:
                    panelProgress2.Visible = true;
                    listViewFirmwares.Enabled = false;
                    labelProgress2.Text = Resources.Strings.deleting;
                    break;

                case Messages.MSG_DELETE_UPDATE_FILE_SUCCESS:

                    OTAManager.UpdateFileList.Remove(updateId);
                    ota.SaveUpdateFileList(modelId);

                    listViewFirmwares.Enabled = true;
                    panelProgress2.Visible = false;
                    break;

                case Messages.MSG_DELETE_UPDATE_FILE_FAIL:
                    listViewFirmwares.Enabled = true;
                    panelProgress2.Visible = false;

                    break;

                case Messages.MSG_DELETE_DEVICE_DIRECTORY_START:

                    panelProgress.Visible = true;
                    listViewDevices.Enabled = false;
                    labelProgress.Text = Resources.Strings.deleting;
                    break;
                case Messages.MSG_DELETE_DEVICE_DIRECTORY_SUCCESS:

                    panelProgress.Visible = false;
                    listViewDevices.Enabled = true;
                    OTAManager.DeviceList.Remove(modelId);
                    ota.SaveDeviceList();
                    break;
                case Messages.MSG_DELETE_DEVICE_DIRECTORY_FAIL:
                    panelProgress.Visible = false;
                    listViewDevices.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        public void RefreshUpdateList()
        {
            listViewFirmwares.Items.Clear();

            btnDeleteUpdateItem.Enabled = false;
            btnEditUpdateItem.Enabled = false;

            this.updateId = null;
            if (updates != null)
            {
                foreach (UpdateFileInfo update in updates.Values)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = update.Id;
                    item.SubItems.Add(update.FileName);
                    item.SubItems.Add(update.Description);
                    item.SubItems.Add(Utils.GetReadableFileSize(update.FileSize));
                    item.SubItems.Add(update.Md5);
                    item.SubItems.Add(Utils.ConvertToLocalTime(update.ReleaseTime).ToString());
                    item.SubItems.Add(update.Version.ToString());
                    item.SubItems.Add(update.Enable ? Resources.Strings.enabled : Resources.Strings.disabled);
                    item.Tag = update;
                    listViewFirmwares.Items.Add(item);

                }

                listViewFirmwares.Sort();
            }
            
        }

        public void RefreshDeviceList()
        {
            listViewDevices.Items.Clear();
            btnDeleteDevice.Enabled = false;
            btnEditDevice.Enabled = false;
            btnLoadUpdateFileList.Enabled = false;
            this.modelId = null;

            if (OTAManager.DeviceList != null)
            {
                foreach (DeviceInfo device in OTAManager.DeviceList.Values)
                {
                    string str = device.ToString();
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = device.Id.ToString();
                    item.SubItems.Add(device.Name);
                    item.SubItems.Add(device.Client);
                    item.SubItems.Add(device.Description);
                    item.SubItems.Add(Utils.ConvertToLocalTime(device.CreateTime).ToString());
                    item.SubItems.Add(device.Enable ? Resources.Strings.enabled : Resources.Strings.disabled);

                    item.Tag = device;
                    listViewDevices.Items.Add(item);
                }
                listViewDevices.Sort();
            }

        }

        private void btnAddDevice_Click(object sender, EventArgs e)
        {
            AddDevice add = new AddDevice();
            if (add.ShowDialog() == DialogResult.OK)
            {
                ota.SaveDeviceList();
            }
        }

        private void btnDeleteDevice_Click(object sender, EventArgs e)
        {

            
            DeleteDeviceItem();
        }


        private void DeleteDeviceItem()
        {

            if (listViewDevices.SelectedItems.Count == 1)
            {

                DialogResult result = MessageBox.Show(Resources.Strings.deleteDevice, Resources.Strings.deleteConfirm, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.OK)
                {
                    ota.DeleteDeviceDirectory(modelId);
                }
            }

        }

        private void EditDeviceItem()
        {
            ListViewItem item = listViewDevices.SelectedItems.Count == 0 ? null : listViewDevices.SelectedItems[0];

            if (item != null)
            {
                DeviceInfo device = (DeviceInfo)item.Tag;
                DeviceEditor editor = new DeviceEditor(device);
                DialogResult result = editor.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.RefreshDeviceList();
                    ota.SaveDeviceList();
                }
            }

        }

        private void LoadUpdateFileList()
        {
            ota.LoadUpdateFileList(modelId);
        }

        private void listViewDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool selected = !(listViewDevices.SelectedIndices.Count == 0);
            btnDeleteDevice.Enabled = selected;
            btnLoadUpdateFileList.Enabled = selected;
            btnEditDevice.Enabled = selected;
            this.modelId = selected ? listViewDevices.SelectedItems[0].SubItems[0].Text : "";
        }

        private void btnEditDevice_Click(object sender, EventArgs e)
        {
            EditDeviceItem();
        }

        private void btnRefreshDevice_Click(object sender, EventArgs e)
        {
            ota.LoadDeviceList();
        }



        private void Main_Load(object sender, EventArgs e)
        {
           
        }

        private void ApplyResource()
        {
            System.ComponentModel.ComponentResourceManager res = new ComponentResourceManager(typeof(Main));
            foreach (Control ctl in Controls)
            {
                res.ApplyResources(ctl, ctl.Name);
            }

        }

        private void listViewDevices_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.LoadUpdateFileList();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            Upload upload = new Upload(modelId);

            if (upload.ShowDialog() == DialogResult.OK)
            {
                ota.SaveUpdateFileList(modelId);
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }

        private void btnLoadUpdateFileList_Click(object sender, EventArgs e)
        {
            this.LoadUpdateFileList();
        }

        private void toolStripMenuItemExportConnfig_Click(object sender, EventArgs e)
        {
            ExportConfig export = new ExportConfig(modelId);
            export.ShowDialog();
        }

        private void toolStripMenuItemUpdateMgr_Click(object sender, EventArgs e)
        {
            ota.LoadUpdateFileList(modelId);
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            DeleteDeviceItem();
        }

        private void toolStripMenuItemEdit_Click(object sender, EventArgs e)
        {
            EditDeviceItem();
        }

        private void listViewFirmwares_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool selected = !(listViewFirmwares.SelectedIndices.Count == 0);

            btnDeleteUpdateItem.Enabled = selected;
            btnEditUpdateItem.Enabled = selected;
            this.updateId = selected ? listViewFirmwares.SelectedItems[0].SubItems[0].Text : "";
        }


        private void toolStripMenuItemShowDetail_Click(object sender, EventArgs e)
        {
            Detail detail = new Detail(modelId);
            detail.ShowDialog();
        }

        private void btnDeleteUpdateItem_Click(object sender, EventArgs e)
        {
            DeleteUpdateFile();
        }

        private void DeleteUpdateFile()
        {

            if (listViewFirmwares.SelectedItems.Count == 1)
            {

                DialogResult result = MessageBox.Show(Resources.Strings.deleteUpdateFile, Resources.Strings.deleteConfirm, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.OK)
                {
                    UpdateFileInfo updateFileInfo = (UpdateFileInfo)listViewFirmwares.SelectedItems[0].Tag;
                    ota.DeleteUpdateFile(modelId, updateFileInfo.FileName);
                }

            }
        }

        private void toolStripMenuItemDeleteUpdateItem_Click(object sender, EventArgs e)
        {
            DeleteUpdateFile();
        }

        private void toolStripMenuItemShowUpdateDetail_Click(object sender, EventArgs e)
        {
            Detail detail = new Detail(modelId, updateId);
            detail.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listViewDevices.SelectedItems.Count == 1)
            {
                DeviceInfo device = (DeviceInfo)listViewDevices.SelectedItems[0].Tag;

                toolStripMenuItemToggleOta.Text = device.Enable ? Resources.Strings.disable : Resources.Strings.enable;

                toolStripMenuItemToggleOta.Tag = device.Enable;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            if (listViewFirmwares.SelectedItems.Count == 1)
            {
                UpdateFileInfo updateInfo = (UpdateFileInfo)listViewFirmwares.SelectedItems[0].Tag;
                toolStripMenuItemEnableUpdate.Text = updateInfo.Enable ? Resources.Strings.disable : Resources.Strings.enable;
                toolStripMenuItemEnableUpdate.Tag = updateInfo.Enable;

            }
            else
            {
                e.Cancel = true;
            }
        }

        private void toolStripMenuItemToggleOta_Click(object sender, EventArgs e)
        {
            if (listViewDevices.SelectedItems.Count == 1)
            {
                DeviceInfo device = (DeviceInfo)listViewDevices.SelectedItems[0].Tag;

                device.Enable = !(bool)toolStripMenuItemToggleOta.Tag;

                ota.SaveDeviceList();

            }

        }


        private void btnEditUpdateItem_Click(object sender, EventArgs e)
        {

            this.EditUpdateFileInfo();
        }


        private void EditUpdateFileInfo()
        {

            if (listViewFirmwares.SelectedItems.Count == 1)
            {


                UpdateFileInfo update = (UpdateFileInfo)listViewFirmwares.SelectedItems[0].Tag;
                UpdateFileEditor editor = new UpdateFileEditor(update);
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    ota.SaveUpdateFileList(modelId);
                }
            }
        }

        private void btnRefreshUpdates_Click(object sender, EventArgs e)
        {
            ota.LoadUpdateFileList(modelId);
        }

        private void toolStripMenuItemEditUpdate_Click(object sender, EventArgs e)
        {
            this.EditUpdateFileInfo();
        }

        private void toolStripMenuItemEnableUpdate_Click(object sender, EventArgs e)
        {
            if (listViewFirmwares.SelectedItems.Count == 1)
            {
                UpdateFileInfo update = (UpdateFileInfo)listViewFirmwares.SelectedItems[0].Tag;

                update.Enable = !(bool)toolStripMenuItemEnableUpdate.Tag;
                ota.SaveUpdateFileList(modelId);
            }


        }

        private void listViewFirmwares_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
        }

        private void tabPageFirmwareList_Leave(object sender, EventArgs e)
        {

        }

        private void tabPageFirmwareList_Click(object sender, EventArgs e)
        {

        }




    }
}
