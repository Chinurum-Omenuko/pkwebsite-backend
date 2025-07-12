import express from 'express';
import cors from 'cors';
import bodyParser from 'body-parser';
import dotenv from 'dotenv';
import pkg from 'node-mailjet';



dotenv.config();

const app = express();
const PORT = 8080;


const allowedOrigins = [
  'https://pithonkids.com',
  'http://localhost:3000',
];

const corsOptions = {
  origin: function (origin, callback) {
    if (!origin || allowedOrigins.includes(origin)) {
      callback(null, true);
    } else {
      callback(new Error('Not allowed by CORS'));
    }
  },
  methods: ['GET', 'POST', 'OPTIONS'],
  allowedHeaders: ['Content-Type', 'Authorization'],
  credentials: true
};


app.use(cors(corsOptions));
app.use(express.json());


// Handle OPTIONS preflight requests globally
app.options('*', cors(corsOptions));




app.options('*', (req, res) => {
  res.sendStatus(200);
});

const mailjet = pkg.apiConnect(
    process.env.MJ_APIKEY_PUBLIC,
    process.env.MJ_APIKEY_PRIVATE
);

app.get('/', async (req, res) => {
    res.send('Mailjet API is running')
})


app.post('/api/send-mail', async (req, res) => {
    try {
        const { firstName, lastName, email, message } = req.body;
        const response = await mailjet
            .post("send", {'version': 'v3.1'})
            .request({
            "Messages": [{
                "From": {
                "Email": "ediks05@gmail.com",
                "Name": "Website Form"
                },
                "To": [{
                "Email": process.env.RECIPIENT_EMAIL,
                "Name": process.env.RECIPIENT_NAME
                }],
                "ReplyTo": {
                "Email": email,
                "Name": `${firstName} ${lastName}`
                },
                "Subject": `Service registration request from ${firstName} ${lastName}`,
                "TextPart": `Message: ${message}\n\nFrom: ${firstName} ${lastName} <${email}>`,
                "HTMLPart": `
                <p>${message}</p>
                <p>From: <strong>${firstName} ${lastName}</strong> &lt;${email}&gt;</p>
                `
            }]
            });


        console.log("Email sent successfully");
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

// Listen on all interfaces (0.0.0.0) for Cloud Run
app.listen(PORT, '0.0.0.0', () => {
    console.log(`Server running on port ${PORT}`);
});