using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;



//Robin added
using System.IO;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

using Emgu.CV;
using Emgu.CV.UI;


//Revision History
// Version 2.1   TakePhotoButton_Click: clear the output window first before showing the result.
// Version 2.0   same as Version 1.5
// Version 1.5   In taking a snaphshot, created timestamped photos and clean them  up when app start
// Version 1.4   Taking a snapshot. fixing some crash by disabling the takephoto button
// 



// This example is from https://www.microsoft.com/cognitive-services/en-us/face-api/documentation/Tutorials/FaceAPIinCSharpTutorial


namespace faceDetection
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		#region Fields

		string filePath = @"C:\TestPhotos\SingleImage\";    // filePath is the image file name, I want to skip this method


		/// <summary>
		/// Temporary group name for create person database
		/// </summary>
		public static readonly string SampleGroupName = Guid.NewGuid().ToString();

		public static readonly string personGroupId = Guid.NewGuid().ToString();

        // for use in the TakePhotoButton_Click method. Allow to delete photo only when this application start time.
        public bool deletephoto = true;


        /// <summary>
        /// Gets group name
        /// </summary>
        public string GroupName
		{
			get
			{
				return SampleGroupName;
			}
		}



		//    static FaceServiceClient faceServiceClient = new FaceServiceClient("1566a7b789cf4c39b50b1b306f707c76");
		// New Subscription Key April 19,2017
		static FaceServiceClient faceServiceClient = new FaceServiceClient("0a61f1f97a7a45fd99327cc919c5d5ee");



		#endregion Fields


		#region Methods

		public MainWindow()
		{
			InitializeComponent();

			// Disable some buttons first
			BrowseButton2.IsEnabled = false;
			TakePhotoButton.IsEnabled = false;
			TrainButton.IsEnabled = false;

			myoutputBox.Text += " Status: \n";
			myoutputBox.Text += "* ============================================================ \n";


			//Clear the outputPane first.
			myoutputBox.Text += "\n";


		}
		// Robin Added :Now you are ready to call the Face API from your application.


		/// <summary>
		//////////////  
		///  Button click triggers Create a group, add friends to that group, bind photos to each person
		/// </summary>
		// --------- Robin Added for the _Click -----------------
		public async void BrowseButton_Click1(object sender, RoutedEventArgs e)
		{

			// ======================================================================================================================

			/*
                        // Decalare Variables 
                        bool groupExists = false;

                        // Below is to open a Dialog to get the file name, pop up the dialog box: openDlg.ShowDialog(this);
                        // Eventually skip the open dialog, and just go read the files directly
                        var openDlg = new Microsoft.Win32.OpenFileDialog();

                        openDlg.Filter = "JPEG Image(*.jpg)|*.jpg";
                        bool? result = openDlg.ShowDialog(this);

                        if (!(bool)result)
                        {
                            return;
                        }

                        */


			/*
                        // Step 1 : getting those photos directories and its photo
                        // hard code the root folder for all image sub folder, it starts in this folder and search down :
                        //                C:\TestPhotos\SingleImage
                        // store the file name
                        string filePath = openDlg.FileName;    // filePath is the image file name, I want to skip this method

                        // example to get its folder name
                        FileInfo fInfo = new FileInfo(filePath);
                        String FolderName = fInfo.Directory.Name;



                        //  example to get the Full path of that file
                        String tmprootFolderName = System.IO.Path.GetDirectoryName(filePath);  // this is the root folder
                        System.IO.DirectoryInfo rootFolderName = new System.IO.DirectoryInfo(tmprootFolderName);  // this is the root folder


                        myoutputBox.Text += "Folder Name = "+ FolderName + "      File Name = "+ filePath+ " \n";
                        myoutputBox.Text += "Root Folder Name = " + tmprootFolderName + "   File Name = " + filePath + " \n";
            */

			/*
                        //////////////////////  group creation

                        // Step 2 : Test whether the group already exists
                        try
                        {
                            myoutputBox.Text += "* Request: GroupID : " + GroupName + " will be used for build person database. \n       Checking whether group " + GroupName + " exists...\n";

                            await faceServiceClient.GetPersonGroupAsync(GroupName);

                            groupExists = true;
                            myoutputBox.Text += "* Response: Group " + GroupName + " exists. \n";
                        }
                        catch (FaceAPIException ex)
                        {
                            if (ex.ErrorCode != "PersonGroupNotFound")
                            {
                                myoutputBox.Text += "* Response: " + ex.ErrorCode + " : " + ex.ErrorMessage + "\n";
                                return;
                            }
                            else
                            {
                                myoutputBox.Text += "* Response: GroupID " + GroupName + " does not exist before. \n";
                            }
                        }

                        //  Well, if that GroupID already exist, first Delete it
                        if (groupExists)
                        {
                            myoutputBox.Text += "* Response: GroupID " + GroupName + " exists before. We are deleting it to start a new Group ID\n";
                            await faceServiceClient.DeletePersonGroupAsync(GroupName);
                        }

                        else   // group not exist, use the new groupId to create the new group, usually this is the case.
                        {
                            // Call create person group REST API
                            // Create person group API call will failed if group with the same name already exists
                            myoutputBox.Text += "* Request: Creating group with GroupID  " + GroupName + " \n";
                            try
                            {
                                await faceServiceClient.CreatePersonGroupAsync(GroupName, GroupName);
                                myoutputBox.Text += "* Response: Success. Group ID " + GroupName + " created \n";
                            }
                            catch (FaceAPIException ex)
                            {
                                myoutputBox.Text += "* Response: " + ex.ErrorCode + " : " + ex.ErrorMessage + "\n";

                                return;
                            }

                            ////////////////////// End  group creation
                        }


            */


			/****************

                        // ======================================================================================================================
                        //            myoutputBox.Text += " A Moment Please ...    \n";


                        // Create an empty person group
                        //          string personGroupId = "myfriends";

                        // Define WongChiMan


                                  CreatePersonResult friend1 = await faceServiceClient.CreatePersonAsync(
                                      // Id of the person group that the person belonged to
                                      personGroupId,
                                      // Name of the person
                                      "Wong Chi Man"
                                  );


                                  // Define WongSumWai and TsangChiWai in the same way
                                  // Define Wong Sum Wing


                                  CreatePersonResult friend2 = await faceServiceClient.CreatePersonAsync(
                                      // Id of the person group that the person belonged to
                                      personGroupId,
                                      // Name of the person
                                      "Wong Sum Wing"
                                  );

                                  // Define TsangChiWai

                                  CreatePersonResult friend3 = await faceServiceClient.CreatePersonAsync(
                                      // Id of the person group that the person belonged to
                                      personGroupId,
                                      // Name of the person
                                      "Tsang Chi Wai"
                                  );

                                  // Define Robin

                                  CreatePersonResult friend4 = await faceServiceClient.CreatePersonAsync(
                                      // Id of the person group that the person belonged to
                                      personGroupId,
                                      // Name of the person
                                      "Robin"
                                  );

                       //           myoutputBox.Text += " There are 4 friends I know:    \n" ;


                                  // Directory contains image files of WongChiMan

                                  const string friend1ImageDir = @"C:\TestPhotos\SingleImage\WongChiMan\";

                                  foreach (string imagePath in Directory.GetFiles(friend1ImageDir, "*.jpg"))
                                  {
                                      using (Stream s = File.OpenRead(imagePath))
                                      {
                                          // Detect faces in the image and add to Anna
                                          await faceServiceClient.AddPersonFaceAsync(
                                              personGroupId, friend1.PersonId, s);
                                      }
                                  }
                        //          myoutputBox.Text += " Wong Chi Man,  ";

                                  // Do the same for WongSumWai and TsangChiWai


                                  // Directory contains image files of Anna

                                  const string friend2ImageDir = @"C:\TestPhotos\SingleImage\WongSumWai\";

                                  foreach (string imagePath in Directory.GetFiles(friend2ImageDir, "*.jpg"))
                                  {
                                      using (Stream s = File.OpenRead(imagePath))
                                      {
                                          // Detect faces in the image and add to WongSumWai
                                          await faceServiceClient.AddPersonFaceAsync(
                                              personGroupId, friend2.PersonId, s);
                                      }
                                  }
                       //           myoutputBox.Text += " Wong Sum Wing,    ";


                                  // Directory contains image files of Anna

                                  const string friend3ImageDir = @"C:\TestPhotos\SingleImage\TsangChiWai\";

                                  foreach (string imagePath in Directory.GetFiles(friend3ImageDir, "*.jpg"))
                                  {
                                      using (Stream s = File.OpenRead(imagePath))
                                      {
                                          // Detect faces in the image and add to TsangChiWai
                                          await faceServiceClient.AddPersonFaceAsync(
                                              personGroupId, friend3.PersonId, s);
                                      }
                                  }
                       //           myoutputBox.Text += " Tsang Chi Wai,    " ;



                                  // Directory contains image files of Anna

                                  const string friend4ImageDir = @"C:\TestPhotos\SingleImage\Robin\";

                                  foreach (string imagePath in Directory.GetFiles(friend4ImageDir, "*.jpg"))
                                  {
                                      using (Stream s = File.OpenRead(imagePath))
                                      {
                                          // Detect faces in the image and add to Robin
                                          await faceServiceClient.AddPersonFaceAsync(
                                              personGroupId, friend4.PersonId, s);
                                      }
                                  }
                       //           myoutputBox.Text += " Robin     \n";


                        ********/

			// Create a Group
			await faceServiceClient.CreatePersonGroupAsync(personGroupId, "MydearFriends");
			//           await faceServiceClient.GetPersonGroupAsync(personGroupId);
			//                   myoutputBox.Text += "* Response: Group ID :" + personGroupId + "  \n";

			//          System.Threading.Thread.Sleep(10000);

			//example to get its folder name
			FileInfo fInfo = new FileInfo(filePath);
			String FolderName = fInfo.Directory.Name;

			String tmprootFolderName = System.IO.Path.GetDirectoryName(filePath);  // this is the root folder
			System.IO.DirectoryInfo rootFolderName = new System.IO.DirectoryInfo(tmprootFolderName);  // this is the root folder
																									  //           myoutputBox.Text += "Folder Name = " + FolderName + "      File Name = " + filePath + " \n";
																									  //           myoutputBox.Text += "Root Folder Name = " + tmprootFolderName +"\n";
			myoutputBox.Text += " A Moment Please ...    \n";

			myoutputBox.Text += " These are the friends I know:    \n";

			// Get the names of the sub-Folder and add to the Group
			foreach (var subdir in System.IO.Directory.EnumerateDirectories(tmprootFolderName))
			{
				var personName = System.IO.Path.GetFileName(subdir);
				myoutputBox.Text += "(" + personName + ")   ";

				// Construct the image path of the friends
				// e.g.  @"C:\TestPhotos\SingleImage\WongChiMan\"
				//  not    C:\TestPhotos\SingleImage\WongSumWing\
				string friendImageDir = filePath + personName + "\\";

				// call the method  _addPerson(string p_personName, string p_friendImageDir) to add each person into the group 
				// and bind their photo to their name
				// 
				_addPerson(personName, friendImageDir, personGroupId);


			}   // Foreach loop

			myoutputBox.Text += "\n";

			//         System.Threading.Thread.Sleep(10000);
			myoutputBox.Text += " Now you can Press the Train Group Button \n";
			myoutputBox.Text += "\n * ============================================================ \n";

			BrowseButton1.IsEnabled = false;
			TrainButton.IsEnabled = true;

			//        myoutputBox.Text += "* Response: Group ID :" + personGroupId + "  \n";

		}        //  ---------  End Robin Added for the BrowseButton_Click1   ------------------



		////////////// ////////////// ////////////// ////////////// ////////////// ////////////// 
		/// <summary>
		///  Button Click triggers Getting a photo from anybody, then verify if he is one of your friends in the group you have 
		///  gather above.
		/// </summary>
		public async void BrowseButton_Click2(object sender, RoutedEventArgs e)
		{

			var openDlg = new Microsoft.Win32.OpenFileDialog();


			openDlg.Filter = "JPEG Image(*.jpg)|*.jpg";
			bool? result = openDlg.ShowDialog(this);

			if (!(bool)result)
			{
				return;
			}

			string strangerfilePath = openDlg.FileName;
			//          myoutputBox.Text += " Your Photo Path:   " + filePath + "\n";




			Uri fileUri = new Uri(strangerfilePath);
			BitmapImage bitmapSource = new BitmapImage();

			bitmapSource.BeginInit();
			bitmapSource.CacheOption = BitmapCacheOption.None;
			bitmapSource.UriSource = fileUri;
			bitmapSource.EndInit();


			//////// Display the Photo to the screen here
			FacePhoto2.Source = bitmapSource;


			///  Verify if he is one of your friend ---> We name this person stranger
			///  So far you have
			///  Groupname (GroupID
			///  StrangerFaceID
			///  The identify process requires 2 Main Steps, MUST:
			///  1. Detect
			///  2. IdentifySync
			///  Step #1 Detection has been done above (got the StrangerFaceID)

			//          myoutputBox.Text += "* Standby ... Identifying in process .... on FaceId = " + StrangerFaceID  + " Thank you \n";
			myoutputBox.Text += "* Standby ... Identifying in process ....  Thank you \n";



			// Convert the photo to Stream and detect to Getting the faceID from API
			using (Stream imageFileStream = File.OpenRead(strangerfilePath))
			{
				try
				{
					Face[] faces = await faceServiceClient.DetectAsync(imageFileStream, true, true);

					//               Face Strangerface = faces.Single();
					//               var idstranger = Strangerface.FaceId;
					//               StrangerFaceID = String.Format("{0}", idstranger);


					/// Call API to identify if This person is in the group
					var identifyResult = await faceServiceClient.IdentifyAsync(personGroupId, faces.Select(ff => ff.FaceId).ToArray());

					// 
					// get all person in PersonGroup
					for (int i = 0; i < identifyResult.Length; i++)
					{


						if (identifyResult != null && identifyResult.Length > 0)


							// check if identifyResult[i].Candidates.Length has something, if no, dunno this person
							if (identifyResult[i].Candidates.Length > 0) // go ahead say know this person
							{

								for (int p = 0; p < identifyResult[i].Candidates.Length; p++)
								{

									//                               myoutputBox.Text += "Debug 22222   \n";

									//  from strPersonId，these are PersonId in PersonGroup 
									string strPersonId = identifyResult[i].Candidates[p].PersonId.ToString();
									var candidateId = identifyResult[i].Candidates[p].PersonId;
									var person = await faceServiceClient.GetPersonAsync(personGroupId, candidateId);
									myoutputBox.Text += " \n This Person is " + person.Name + "        ** Hello ! I know you.** \n";
								}   // the inner for loop

							}
							else
							{
								myoutputBox.Text += " \n *** The other persons ? Sorry, I dont know you. *** \n";
							}



					}   //outer for loop

				}
				catch
				{
					myoutputBox.Text += "something wrong \n";
				}


				myoutputBox.Text += "* Thank you. The End. \n";



			}   // End Using Stream.




			// Enable the 2nd button 
			//            if (friendFaceID1.Length > 0 && StrangerFaceID.Length > 0)

			BrowseButton2.IsEnabled = false;
			BrowseButton1.IsEnabled = false;



			/// End  Verify if he is one of your friend ---> We name this person stranger



		}        //  ---------  End BrowseButton_Click2   ------------------


        public async void TakePhotoButton_Click(object sender, RoutedEventArgs e)
        {

            //  sample from  http://www.emgu.com/wiki/index.php/Camera_Capture_in_7_lines_of_code
            //         Capture capture = new Capture(); //create a camera captue
            //        Bitmap image = capture.QueryFrame().Bitmap; //take a picture


            // Initialize 

            myoutputBox.Text = "General Information Status: \n";

            FacePhoto2.Source = null;
   //         myoutputBox.Text += deletephoto + "\n";
            if (deletephoto)   //  For this session,to start, clean up the folder so that no temp*.jpg files remain
            {
                    string DeleteThis = "temp";
                    string[] Files = Directory.GetFiles(@"C:\TestPhotos\SingleImage");

                    foreach (string file in Files)
                    {
                        if (file.ToUpper().Contains(DeleteThis.ToUpper()))
                        {
                            File.Delete(file);
                        }
                    }

                    deletephoto = false;

            }  // if deletephoto
               //  **** End Initialize  *****


            ImageViewer viewer = new ImageViewer(); //create an image viewer
            Capture capture = new Capture(); //create a camera captue

            viewer.Image = capture.QueryFrame(); //draw the image obtained from camera
            viewer.ShowDialog(); //show the image viewer
            var mybitmap = viewer.Image.Bitmap;


            //   string filesToDelete = @"C:\TestPhotos\SingleImage\temp*.jpg";   // Only delete jpg files containing "tmp" in their filenames

            //      var strangerPhotoPath = @"C:\TestPhotos\SingleImage\temp.jpg";
            var strangerPhotoPath = @"C:\TestPhotos\SingleImage\temp" + DateTime.Now.ToFileTime() + ".jpg";



            // save a copy and get the path
            mybitmap.Save(strangerPhotoPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            // successful saving the photo to that location

            /******/
            // Set up to Display the photo
            Uri fileUri = new Uri(strangerPhotoPath);
            BitmapImage bitmapSource = new BitmapImage();

            bitmapSource.BeginInit();
            bitmapSource.CacheOption = BitmapCacheOption.None;
            bitmapSource.UriSource = fileUri;
            bitmapSource.EndInit();


            //////// Display the Photo to the screen here
            FacePhoto2.Source = bitmapSource;

           /******/

            // Convert the photo to Stream and detect to Getting the faceID from API
            using (Stream imageFileStream = File.OpenRead(strangerPhotoPath))
			{

				Face[] faces = await faceServiceClient.DetectAsync(imageFileStream, true, true);

				//    Face Strangerface = faces.Single();
				//   var idstranger = Strangerface.FaceId;
				//    StrangerFaceID = String.Format("{0}", idstranger);

				//               myoutputBox.Text += "Got Your FaceId =" + StrangerFaceID + "\n";

				try
				{

					/// Call API to identify if This person is in the group
					var identifyResult = await faceServiceClient.IdentifyAsync(personGroupId, faces.Select(ff => ff.FaceId).ToArray());



					// 
					// get all person in PersonGroup
					for (int i = 0; i < identifyResult.Length; i++)
					{

						//                    myoutputBox.Text += "How many persons dectected by API ? " + identifyResult.Length;
						//                    myoutputBox.Text += ". And how many known persons ? : " + identifyResult[i].Candidates.Length + "\n";
						//                       myoutputBox.Text += "Debug 1111111   \n";

						if (identifyResult != null && identifyResult.Length > 0)


							// check if identifyResult[i].Candidates.Length has something, if no, dunno this person
							if (identifyResult[i].Candidates.Length > 0) // go ahead say know this person
							{

								for (int p = 0; p < identifyResult[i].Candidates.Length; p++)
								{

									// myoutputBox.Text += "Debug 22222   \n";

									// from strPersonId，these are PersonId in PersonGroup 
									string strPersonId = identifyResult[i].Candidates[p].PersonId.ToString();
									var candidateId = identifyResult[i].Candidates[p].PersonId;
									var person = await faceServiceClient.GetPersonAsync(personGroupId, candidateId);

									//                             myoutputBox.Text += "Person is " + strPersonId + "        ** Hello ! I know you.** \n";

									//                              myoutputBox.Text += "Person is " + strPersonId +  "        ** Hello ! I know you.** \n";
									myoutputBox.Text += "\n Hello !  " + person.Name + "        **  I know you.** \n";
								}   // the inner for loop

							}
							else
							{
								myoutputBox.Text += "*** For the other person, Sorry, I dont know you. *** \n";
							}

        //                mybitmap.Dispose();

                    }   //outer for loop


				}   // Try block
				catch
				{
            //        capture.Stop(); mybitmap.Dispose();
                    myoutputBox.Text += "Partial Face is not allowed,  Try Full Face Please";
					myoutputBox.Text += "\n * ============================================================ \n";

                }

                //            capture.Pause();

            //    capture.Stop(); capture.Stop(); mybitmap.Dispose();
                capture.Dispose();


                myoutputBox.Text += "* Thank you. The End. \n";
				myoutputBox.Text += "\n * ============================================================ \n";


                //             TakePhotoButton.IsEnabled = false;



            }   // End Using Stream.
            




        }



        /// <summary>
        /// Define a person into the  group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="personName"></param>9
        /// <param name="imagesPath"></param>
        public async void _addPerson(string p_personName, string p_friendImageDir, string p_personGroupId)
		{

			//             string l_friendImageDir = "\"" + p_friendImageDir + "\"";


			// Create the person in the group
			CreatePersonResult friend = await faceServiceClient.CreatePersonAsync(
			// Id of the person group that the person belonged to
			p_personGroupId,
			// Name of the person
			p_personName);

			// Bind each jpg file in the sub-folder to the person
			int i = 0;

			foreach (string imagePath in Directory.GetFiles(p_friendImageDir, "*.jpg"))
			{


				//            myoutputBox.Text += "  222 " + p_friendImageDir + "\n";
				//          myoutputBox.Text += " \n  333 image path  " + imagePath + "\n";

				using (Stream s = File.OpenRead(imagePath))
				{
					// Detect faces in the image and bind to the friend
					await faceServiceClient.AddPersonFaceAsync(p_personGroupId, friend.PersonId, s);
				}
				i = i + 1;
			}

			myoutputBox.Text += "***"+ p_personName + " has  "; myoutputBox.Text += i; myoutputBox.Text += " photos  \n";


		}




		// Robin Added: 
		/* 
         Step 4: Upload images to detect faces
         The most straightforward way to detect faces is by calling the Face - Detect API 
         by uploading the image file directly. When using the client library, 
         this can be done by using the asynchronous method DetectAsync of FaceServiceClient. 
         Each returned face contains a rectangle to indicate its location, 
         combined with a series of optional face attributes. 
         In this example, we only need to retrieve the face location. 
         Here we need to insert an asynchronous function into the MainWindow class for face detection:            
         */

		private async Task<FaceRectangle[]> UploadAndDetectFaces(string imageFilePath)
		{
			try
			{
				using (Stream imageFileStream = File.OpenRead(imageFilePath))
				{
					var faces = await faceServiceClient.DetectAsync(imageFileStream);
					var faceRects = faces.Select(face => face.FaceRectangle);


					return faceRects.ToArray();
				}
			}
			catch (Exception)
			{
				return new FaceRectangle[0];
			}
		}



		#endregion Methods

		private async void TrainButton_Click(object sender, RoutedEventArgs e)
		{

			myoutputBox.Text += " \n \n * A Moment Please ...Training the group in progress ............... ";

			await faceServiceClient.TrainPersonGroupAsync(personGroupId);

			System.Threading.Thread.Sleep(5000);

			// Train the group
			myoutputBox.Text += "         * Success : Training the group ...............  \n \n";
			myoutputBox.Text += "* Please Click the button *Upload Photo* or *Take a Photo* To Authenticate you \n";
			myoutputBox.Text += "\n * ============================================================ \n";

			BrowseButton2.IsEnabled = true;
			TakePhotoButton.IsEnabled = true;
			TrainButton.IsEnabled = false;




		}
	}


}
