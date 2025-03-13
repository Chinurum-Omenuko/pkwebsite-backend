
import express from 'express';
import cors from 'cors';
import bodyParser from 'body-parser';
import dotenv from 'dotenv';
import pkg from 'node-mailjet';



dotenv.config();


const app = express();
const PORT = process.env.PORT || 3000;

const corsOptions = {
    origin: 'http://127.0.0.1:5501',
    methods: ['POST'],
    allowedHeaders: ['Content-Type'],
    credentials: true
  };


app.use(cors(corsOptions));
app.use(bodyParser.json());


const mailjet = pkg.apiConnect(
    process.env.MJ_APIKEY_PUBLIC,
    process.env.MJ_APIKEY_PRIVATE
)



app.post('/api/send-email', async (req, res) => {
  try {
    const { name, email, message } = req.body;

    const response = await mailjet
      .post("send", {'version': 'v3.1'})
      .request({
        "Messages": [
          {
            "From": {
              "Email": process.env.SENDER_EMAIL,
              "Name": name
            },
            "To": [
              {
                "Email": process.env.RECIPIENT_EMAIL,
                "Name": process.env.RECIPIENT_NAME
              }
            ],
            "Subject": `Contact Form Submission from ${name}`,
            "TextPart": message,
            "HTMLPart": `<p>Name: ${name}<br>Email: ${email}<br>Message: ${message}</p>`
          }
        ]
      });
      console.log("request sent to mailjet api")

    res.json({
      success: true,
      message: 'Email sent successfully',
      data: response.body
    });
  } catch (error) {
    console.error('Error sending email:', error);
    res.status(500).json({
      success: false,
      message: 'Error sending email',
      error: error.message
    });
  }
});

app.listen(PORT, () => {
  console.log(`Server running on port ${PORT}`);
});