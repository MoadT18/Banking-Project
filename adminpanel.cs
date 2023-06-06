using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Banking_Project
{
    public partial class adminpanel : Form
    {
        private System.Drawing.Image selectedImage;
        private string imagePath;
        private string fileName;
        private Account editaccount;
        private Admin admin = new Admin();
        private Account account = new Account();
        private User user = new User();
        public List<String> Users { get; } = new List<String>();

        public adminpanel(Admin newAdmin)
        {

            InitializeComponent();
            

            addUsersToListView();
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // ShowImage();
            labelName.Text = newAdmin.FirstName;


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string userid = selectedItem.SubItems[0].Text; 
                string firstname = selectedItem.SubItems[1].Text;
                string lastname = selectedItem.SubItems[2].Text;
                string email = selectedItem.SubItems[3].Text;


                //MessageBox.Show(userid);
                int ID;
                int.TryParse(userid, out ID);
                editaccount = account.GetAccount2(ID);



                editID.Text = userid;
                editFN.Text = firstname;
                editLN.Text = lastname;
                editEM.Text = email;

                editB.Text = editaccount.Balance.ToString();

                ShowImage();


                

                if(editaccount.AccountTypeID == 1)
                {
                    currentR.Checked = true;
                }
                else
                {
                    savingsR.Checked = true;
                }


            }
        }

        public void addUsersToListView()
        {
          
            listView1.Items.Clear();

            foreach (User user in user.GetUsers())
            {
                ListViewItem listViewItem = new ListViewItem(user.ID.ToString());
                listViewItem.SubItems.Add(user.FirstName);



                listViewItem.SubItems.Add(user.LastName);
                listViewItem.SubItems.Add(user.Email);





                listView1.Items.Add(listViewItem);
            }
        }


        //ADD USER
        private void adduserButton_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(aFN.Text))
            {
                MessageBox.Show("Please enter the first name.");


                return;
            }
            if (string.IsNullOrWhiteSpace(aLN.Text))
            {
                MessageBox.Show("Please enter the last name.");


                return;
            }
            if (string.IsNullOrWhiteSpace(aEM.Text))
            {
                MessageBox.Show("Please enter the email.");


                return;
            }
            
            if (string.IsNullOrWhiteSpace(aPW.Text))
            {
                MessageBox.Show("Please enter the password.");
                return;
            }

            if (user.DoesEmailExist(aEM.Text) || admin.DoesEmailExist2(aEM.Text))
            {
                MessageBox.Show("There is already an account with this email.");

                return;
            }




            if (selectedImage == null)
            {
                MessageBox.Show("You need to select an image first!");
                return;
            }




            else
            {
                int accounttype = 0;

                if (radioButton1.Checked)
                {
                    accounttype = 1;
                }
                else { 
                    accounttype = 2;
                }
                User user = new User(aFN.Text, aLN.Text, aEM.Text, aPW.Text);
                SaveImage(selectedImage);

                Account account = new Account(accounttype, fileName, 0, user.FirstName, user.LastName, user.Email, user.Password);
                Data data = new Data();
                data.insertUserAccount(account, user);

                MessageBox.Show("User added");

                aFN.Text = "";
                aLN.Text = "";
                aEM.Text = "";
                aPW.Text = "";

                pictureBox2.Image = null;




            }


        }

        private void ShowImage()
        {
            // Get the file path of the project directory
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            // Combine the project directory and the Images folder name to create the full path of the folder
            string imagesDirectory = Path.Combine(projectDirectory, "Images");

            // Create a file path for the "moad" image
            string imagePath = Path.Combine(imagesDirectory, editaccount.Photo.ToString());

            // Check if the image file exists
            if (File.Exists(imagePath))
            {
                // Load the image file into an Image object
                System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath);

                // Display the image in a PictureBox control
               
                pictureBox3.Image = image;

            }
            else
            {
                MessageBox.Show("The 'moad' image could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addphoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            openFileDialog1.Title = "Select an image file";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imagePath = openFileDialog1.FileName;

                try
                {
                    // Attempt to load the image file
                    selectedImage = System.Drawing.Image.FromFile(imagePath);

                    // Display the selected image in a PictureBox control
                    pictureBox2.Image = selectedImage;
                }
                catch (Exception ex)
                {
                    // Display an error message if the file format is invalid
                    MessageBox.Show("Invalid file type. Please select a valid image file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

       

        private void SaveImage(System.Drawing.Image image)
        {
            // Get the file path of the project directory
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            // Create a subdirectory called "Images" within the project directory
            string subdirectory = Path.Combine(projectDirectory, "Images");
            Directory.CreateDirectory(subdirectory);

            // Create a file name for the image based on the current date and time
            fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            // Determine the file extension and image format based on the file type of the selected image
            ImageFormat imageFormat;
            switch (Path.GetExtension(imagePath).ToLower())
            {
                case ".bmp":
                    fileName += ".bmp";
                    imageFormat = ImageFormat.Bmp;
                    break;
                case ".jpeg":
                case ".jpg":
                    fileName += ".jpg";
                    imageFormat = ImageFormat.Jpeg;
                    break;
                case ".png":
                    fileName += ".png";
                    imageFormat = ImageFormat.Png;
                    break;
                default:
                    throw new ArgumentException("Invalid file type.");
            }

            // Combine the subdirectory and file name to create the full file path of the image
            string filePath = Path.Combine(subdirectory, fileName);

            // Save the image to the specified file path
            image.Save(filePath, imageFormat);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private List<ListViewItem> originalItems = new List<ListViewItem>();

        private void button1_Click(object sender, EventArgs e)
        {

            string searchTerm = search.Text.ToLower();

            if (originalItems.Count == 0)
            {
                
                originalItems.AddRange(listView1.Items.OfType<ListViewItem>());
            }

            listView1.BeginUpdate();
            listView1.Items.Clear();

            foreach (ListViewItem item in originalItems)
            {
                string email = item.SubItems[3].Text.ToLower(); 

                if (email.Contains(searchTerm))
                {
                    listView1.Items.Add(item);
                }
            }

            listView1.EndUpdate();
        }

        private void settingsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        //EDIT PHOTO
        private void editphotoButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            openFileDialog1.Title = "Select an image file";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imagePath = openFileDialog1.FileName;

                try
                {
                    // Attempt to load the image file
                    selectedImage = System.Drawing.Image.FromFile(imagePath);

                    // Display the selected image in a PictureBox control
                    pictureBox3.Image = selectedImage;
                }
                catch (Exception ex)
                {
                    // Display an error message if the file format is invalid
                    MessageBox.Show("Invalid file type. Please select a valid image file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // EDIT users
        private void edituserButton_Click(object sender, EventArgs e)
        {
           

           
            

            if (string.IsNullOrWhiteSpace(editFN.Text))
            {
                MessageBox.Show("Please enter the first name.");


                return;
            }
            if (string.IsNullOrWhiteSpace(editLN.Text))
            {
                MessageBox.Show("Please enter the last name.");


                return;
            }
            if (string.IsNullOrWhiteSpace(editEM.Text))
            {
                MessageBox.Show("Please enter the email.");


                return;
            }
           
            

            else
            {

                if (selectedImage != null)
                {


                    SaveImage(selectedImage);
                }
                string userid = editID.Text.ToString();
                string fn = editFN.Text;
                string ln = editLN.Text;
                string email = editEM.Text;
                string balance = editB.Text;

                string type;
                if(currentR.Checked == true)
                {
                    type = "1";

                }
                else
                {
                    type = "2";
                }

                user.updateUser2(userid, fn, ln, email);


                if (string.IsNullOrWhiteSpace(fileName))
                {
                    account.updateAccount(userid, balance, type, editaccount.Photo);
                }
                else
                {
                    account.updateAccount(userid, balance, type, fileName);
                }

                MessageBox.Show("User updated");

                addUsersToListView();

                editID.Text = "";
                editFN.Text = "";
                editLN.Text = "";

                editEM.Text = "";
                editB.Text = "";
                currentR.Checked = true;
                pictureBox3.Image = null;

            }
        }


        //Gaat naar create user panel
        private void createuserButton_Click(object sender, EventArgs e)
        {

            //Create user panel
            adduserPanel.Width = 758;
            adduserPanel.Height = 572;
            adduserPanel.Location = new Point(3, 56);

            // Overview panel
            overviewPanel.Width = 758;
            overviewPanel.Height = 10;
            overviewPanel.Location = new Point(3, 603);

        }

        private void overviewButton_Click(object sender, EventArgs e)
        {
            // Overview panel
            overviewPanel.Width = 758;
            overviewPanel.Height = 557;
            overviewPanel.Location = new Point(3, 56);

            //Create user panel
            adduserPanel.Width = 758;
            adduserPanel.Height = 10;
            adduserPanel.Location = new Point(3, 618);
        }

        private void adminpanelButton_Click(object sender, EventArgs e)
        {

            //Admin panel
            panelAdmin.Width = 763;
            panelAdmin.Height = 633;
            panelAdmin.Location = new Point(188, 0);


            //Create user panel
            adduserPanel.Width = 758;
            adduserPanel.Height = 10;
            adduserPanel.Location = new Point(3, 618);


            // Overview panel
            overviewPanel.Width = 758;
            overviewPanel.Height = 10;
            overviewPanel.Location = new Point(3, 603);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //Admin panel
            panelAdmin.Width = 763;
            panelAdmin.Height = 633;
            panelAdmin.Location = new Point(188, 0);


            //Create user panel
            adduserPanel.Width = 758;
            adduserPanel.Height = 10;
            adduserPanel.Location = new Point(3, 618);


            // Overview panel
            overviewPanel.Width = 758;
            overviewPanel.Height = 10;
            overviewPanel.Location = new Point(3, 603);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            //Admin panel
            panelAdmin.Width = 763;
            panelAdmin.Height = 633;
            panelAdmin.Location = new Point(188, 0);


            //Create user panel
            adduserPanel.Width = 758;
            adduserPanel.Height = 10;
            adduserPanel.Location = new Point(3, 618);


            // Overview panel
            overviewPanel.Width = 758;
            overviewPanel.Height = 10;
            overviewPanel.Location = new Point(3, 603);
        }

        //Close app
        private void button2_Click(object sender, EventArgs e)
        {
            admin = null;
            loginForm form = new loginForm();

            form.Show();
            this.Hide();
        }


        //DELETE USER
        private void deleteuserButton_Click(object sender, EventArgs e)
        {
            string userid = deleteuserID.Text;

            if (string.IsNullOrWhiteSpace(userid))
            {
                MessageBox.Show("Enter the userid");
                return;
            }


            int accountid = account.getAccountID(userid);

            account.deActivate2(accountid);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
                openFileDialog1.Title = "Select an image file";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog1.FileName;

                    try
                    {
                        // Attempt to load the image file
                        selectedImage = System.Drawing.Image.FromFile(imagePath);

                        // Display the selected image in a PictureBox control
                        pictureBox3.Image = selectedImage;
                    }
                    catch (Exception ex)
                    {
                        // Display an error message if the file format is invalid
                        MessageBox.Show("Invalid file type. Please select a valid image file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            
        }

        public void GeneratePDF()
        {
            Document document = new Document();

            // Set the path where the PDF file will be saved
            string path = "users_list.pdf";

            // Create a new PDF writer
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));

            document.Open();

            // Create a new table with the same number of columns as the ListView
            PdfPTable table = new PdfPTable(listView1.Columns.Count);

            // Add the column headers to the table
            foreach (ColumnHeader column in listView1.Columns)
            {
                table.AddCell(column.Text);
            }

            // Add the items from the ListView to the table
            foreach (ListViewItem item in listView1.Items)
            {
                foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                {
                    table.AddCell(subItem.Text);
                }
            }

            // Add the table to the document
            document.Add(table);

            document.Close();

            // Open the file for download
            System.Diagnostics.Process.Start(path);
        }

        private void downloadT_Click(object sender, EventArgs e)
        {
            GeneratePDF();

        }
    }
}
