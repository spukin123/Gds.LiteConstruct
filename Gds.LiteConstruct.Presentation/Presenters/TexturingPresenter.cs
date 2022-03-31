using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Gds.LiteConstruct.Core.Presenters;
using Gds.LiteConstruct.Core.Controllers;
using Gds.Windows;
using Gds.LiteConstruct.Environment;
using Gds.LiteConstruct.BusinessObjects;
using Gds.LiteConstruct.Windows.WorkItems;
using Gds.LiteConstruct.Windows.Commands;
using Gds.Runtime;
using Gds.LiteConstruct.Presentation.Services;

namespace Gds.LiteConstruct.Presentation.Presenters
{
    public partial class TexturingPresenter : UserControl, ITexturingPresenter, IWorkItemComponent
    {
        public TexturingPresenter()
        {
            InitializeComponent();
			InitializeIcons();
            texturesCategoryBindingSource.CurrentItemChanged += OnCmbCategoriesSelectedIndexChanged;

            lbTextures.DoubleClick += OnLbTexturesDoubleClick;
            EnableRotationControl(false);
        }

		private void InitializeIcons()
		{
			tsbAddCategory.Image = Icons.Folders.New.ToBitmap16();
			tsbCategoryProperties.Image = Icons.Folders.Properties.ToBitmap16();
			tsbDeleteCategory.Image = Icons.Folders.Delete.ToBitmap16();

			tsbAddTexture.Image = Icons.Files.New.ToBitmap16();
			tsbTextureProperties.Image = Icons.Files.Properties.ToBitmap16();
			tsbDeleteTexture.Image = Icons.Files.Delete.ToBitmap16();

			tsbApplyTexture.Image = Icons.Files.Check.ToBitmap16();
			tsbMarkAsDefault.Image = Icons.Actions.Tick.ToBitmap16();

			miApply.Image = Icons.Files.Check.ToBitmap16();
			miApplyToAll.Image = Gds.LiteConstruct.Presentation.Properties.Resources.Fill;
			miMakeDefault.Image = Icons.Actions.Tick.ToBitmap16();
			miProperties.Image = Icons.Files.Properties.ToBitmap16();
			miDelete.Image = Icons.Files.Delete.ToBitmap16();
			miPreview.Image = Icons.Files.Preview.ToBitmap16();
		}

		#region IWorkItemComponent Members

		private CommandHolder commands;

		public void AddedToWorkItem(WorkItem workItem)
		{
			workItem.Commands[TexturingCommands.ApplyTexture].AddInvoker(tsbApplyTexture);
			workItem.Commands[TexturingCommands.ApplyToAll].AddInvoker(tsbApplyToAll);
			workItem.Commands[TexturingCommands.NewTexture].AddInvoker(tsbAddTexture);
			workItem.Commands[TexturingCommands.TextureProperties].AddInvoker(tsbTextureProperties);
			workItem.Commands[TexturingCommands.DeleteTexture].AddInvoker(tsbDeleteTexture);
			workItem.Commands[TexturingCommands.MakeDefault].AddInvoker(tsbMarkAsDefault);

			workItem.Commands[TexturingCommands.NewCategory].AddInvoker(tsbAddCategory);
			workItem.Commands[TexturingCommands.CategoryProperties].AddInvoker(tsbCategoryProperties);
			workItem.Commands[TexturingCommands.DeleteCategory].AddInvoker(tsbDeleteCategory);

			workItem.Commands[TexturingCommands.ApplyTexture].AddInvoker(miApply);
			workItem.Commands[TexturingCommands.ApplyToAll].AddInvoker(miApplyToAll);
			workItem.Commands[TexturingCommands.TextureProperties].AddInvoker(miProperties);
			workItem.Commands[TexturingCommands.DeleteTexture].AddInvoker(miDelete);
			workItem.Commands[TexturingCommands.MakeDefault].AddInvoker(miMakeDefault);
			workItem.Commands[TexturingCommands.Preview].AddInvoker(miPreview);

			workItem.Commands[TexturingCommands.ApplyTexture].Execute += ApplyTextureToSelectedSide;
			workItem.Commands[TexturingCommands.ApplyToAll].Execute += ApplyTextureToAllSides;
			workItem.Commands[TexturingCommands.NewTexture].Execute += AddTexture;
			workItem.Commands[TexturingCommands.TextureProperties].Execute += ShowTextureProperties;
			workItem.Commands[TexturingCommands.DeleteTexture].Execute += DeleteTexture;
			workItem.Commands[TexturingCommands.MakeDefault].Execute += MakeDefaultTexture;
			workItem.Commands[TexturingCommands.Preview].Execute += PreviewTexture;

			workItem.Commands[TexturingCommands.NewCategory].Execute += AddCategory;
			workItem.Commands[TexturingCommands.CategoryProperties].Execute += ShowCategoryProperties;
			workItem.Commands[TexturingCommands.DeleteCategory].Execute += DeleteCategory;

			commands = workItem.Commands;
		}

		public void RemovedFromWorkItem(WorkItem workItem)
		{
		}

		#endregion

		#region Categories Actions

		private void AddCategory()
        {
            CategoryPropertiesForm form = new CategoryPropertiesForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                controller.CreateTexturesCategory(form.CategoryName);
                UpdateTexturesCategories();
            }
        }

        private void ShowCategoryProperties()
        {
            CategoryPropertiesForm form = new CategoryPropertiesForm(selectedCategory.Name);
            if (form.ShowDialog() == DialogResult.OK)
            {
                selectedCategory.Name = form.CategoryName;
				UpdateTexturesCategories();
            }
        }

        private void DeleteCategory()
        {
			if (MessageWindow.Question("Are you sure you want to delete selected category?") == DialogResult.No)
				return;

            try
            {
                texturesEnvironment.RemoveCategory(selectedCategory);
                UpdateTexturesCategories();
            }
            catch (Exception ex)
            {
                MessageWindow.Error(ex.Message);
            }
		}

		#endregion

		private bool binded = true;

        private void OnNumUpDownAngleValueChanged(object sender, EventArgs e)
        {
            if (binded)
            {
                binded = false;

                if ((int)numericUpDownAngle.Value == 360 || (int)numericUpDownAngle.Value == -360)
                {
                    numericUpDownAngle.Value = (decimal)0;
                }
                controller.RotateTextureOnSelectedSide(Angle.FromDegrees((int)numericUpDownAngle.Value));

                binded = true;
            }
        }

        #region ITexturizingPresenter Members

        private ITexturingController controller;

        public ITexturingController TexturizeManagerController
        {
            set { controller = value; }
        }

        private bool texturingEnabled;

        public void EnableTexturing(bool enable)
        {
            texturingEnabled = enable;

            if (selectedCategory == null || selectedCategory.Textures.Length == 0)
				commands[TexturingCommands.ApplyTexture].Enabled = false;
            else
                commands[TexturingCommands.ApplyTexture].Enabled = enable;
        }

        private TexturesCategory selectedCategory;

        private TexturesEnvironment texturesEnvironment;

        public TexturesEnvironment TexturesEnvironment
        {
            set { texturesEnvironment = value; }
        }

        public void EnableRotationControl(bool state)
        {
            numericUpDownAngle.Enabled = state;
        }

        public Angle RotationAngle
        {
            set { numericUpDownAngle.Value = (decimal)value.Degrees; }
        }

        #endregion

        private void OnLoad(object sender, EventArgs e)
        {
            UpdateTexturesCategories();
        }

        public void UpdateTexturesCategories()
        {
            texturesCategoryBindingSource.DataSource = texturesEnvironment.Categories;
            texturesCategoryBindingSource.ResetBindings(false);
            UpdateSelectedCategory();
        }

        private void OnCmbCategoriesSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectedCategory();
        }

        private void UpdateSelectedCategory()
        {
            selectedCategory = texturesCategoryBindingSource.Current as TexturesCategory;
            if (selectedCategory != null)
            {
				commands[TexturingCommands.NewTexture].Enabled = selectedCategory.AllowTexturesAdding;
				commands[TexturingCommands.TextureProperties].Enabled = selectedCategory.AllowTexturesEditing;
				commands[TexturingCommands.DeleteTexture].Enabled = selectedCategory.AllowTexturesRemoving;
                tsbDeleteCategory.Enabled = selectedCategory.AllowRemoving;
                tsbCategoryProperties.Enabled = selectedCategory.AllowEditing;
                UpdateTexturesList(false);
                UpdateDefaultTexture();
            }
        }

        private void UpdateTexturesList()
        {
            UpdateTexturesList(true);
        }

        private void UpdateTexturesList(bool checkStates)
        {
            TextureInfo[] textures = selectedCategory.Textures;
            textureBindingSource.DataSource = textures;

            bool state = true;
            if (textures.Length == 0)
                state = false;

            if (state == false || checkStates)
            {
				commands[TexturingCommands.TextureProperties].Enabled = state;
				commands[TexturingCommands.DeleteTexture].Enabled = state;
            }

			commands[TexturingCommands.MakeDefault].Enabled = state;
            if (state == false || texturingEnabled == false)
				commands[TexturingCommands.ApplyTexture].Enabled = false;
            else
				commands[TexturingCommands.ApplyTexture].Enabled = true;
        }

        private void UpdateDefaultTexture()
        {
            lblDefaultTexture.Text = texturesEnvironment.DefaultTextureName;
        }

        private TextureInfo SelectedTexture
        {
            get { return lbTextures.SelectedItem as TextureInfo; }
		}

		#region Texture Actions

		private void AddTexture()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.bmp;*.png;*.jpg;*.jpeg;*.gif)|*.bmp;*.png;*.jpg;*.jpeg;*.gif";
            dialog.Title = "Open image";
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName))
            {
                try
                {
                    selectedCategory.Add(dialog.FileName);
                    UpdateTexturesList();
                }
                catch (FileExistsException)
                {
                    MessageWindow.Warning("Current file exists in your list", "Failed to add");
                    return;
                }
                UpdateTexturesList();
            }
        }

        private void DeleteTexture()
        {
            if (MessageWindow.Question("Are you sure you want to delete selected texture?") == DialogResult.No)
                return;

            try
            {
                selectedCategory.Remove(SelectedTexture);
                UpdateTexturesList();
            }
            catch (Exception ex)
            {
                MessageWindow.Error(ex.Message);
            }
        }

        private void ApplyTextureToSelectedSide()
        {
            controller.ApplyTextureToSelectedSide(SelectedTexture.Id);
            UpdateTexturesList();
        }

        private void ApplyTextureToAllSides()
        {
            controller.ApplyTextureToAllSides(SelectedTexture.Id);
            UpdateTexturesList();
        }

        private void ShowTextureProperties()
        {
            TextureInfo texture = SelectedTexture;
            TexturePropertiesForm form = new TexturePropertiesForm(texture);
            if (form.ShowDialog() == DialogResult.OK)
            {
                selectedCategory.Update(texture, form.Texture);
                UpdateTexturesList();
                UpdateDefaultTexture();
            }
        }

        private void MakeDefaultTexture()
        {
            texturesEnvironment.MakeDefaultTexture(SelectedTexture);
            UpdateDefaultTexture();
        }

		private void PreviewTexture()
		{
			string name = SelectedTexture.Name;
			string location = texturesEnvironment.GetTexturePhysicalPath(SelectedTexture.Id);
            Gds.Runtime.AppContext.Get<ITextureEditService>().Preview(name, location);
		}

        #endregion

		private void OnLbTexturesDoubleClick(object sender, EventArgs e)
		{
			MouseEventArgs mouseArgs = (MouseEventArgs)e;
			if (lbTextures.IndexFromPoint(mouseArgs.Location) != ListBox.NoMatches)
			{
				ApplyTextureToSelectedSide();
			}
		}

        private void OnLbTexturesMouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				int index = lbTextures.IndexFromPoint(e.Location);
				if (index != ListBox.NoMatches)
				{
					lbTextures.SelectedIndex = index;
					cmsTexture.Show(lbTextures, e.Location);
				}
			}
		}

        private void OnLbTexturesSelectedIndexChanged(object sender, EventArgs e)
        {
            //SelectedTexture
        }
	}
}