import react, { useEffect, useState } from 'react';
import axios from 'axios';
import moment from 'moment';
import { BrowserRouter as Router, Switch, Route, Link, useParams } from "react-router-dom";
import { Email, Facebook, Twitter } from 'react-sharingbuttons'
import 'react-sharingbuttons/dist/main.css'
import '../index.css';
import Cookies from 'universal-cookie';

export default function ReviewDetails() {
    // the id of each post
    let { id } = useParams();
    const [Loading, setLoading] = useState(true);
    const [Title, setTitle] = useState('');
    const [About, setAbout] = useState('');
    const [Description, setDescription] = useState('');
    const [Image, setImage] = useState('');
    const [Date, setDate] = useState('');
    const [Category, setCategory] = useState('');
    const [User, setUser] = useState('');
    const [VideoUrl, setVideoUrl] = useState('');
    const cookies = new Cookies();
    const token="Bearer "+ cookies.get('Token');
    const config = { headers: { 'Content-Type': 'multipart/form-data' ,'Authorization': token} };
    // sharing links and texts
    const url = 'http://localhost:3000/ReviewDetails/' + id
    const shareText = Title
    // the url of each image
    const BaseImageUrl = "https://localhost:44352/ReviewImages/";

    useEffect(() => {
        if (id === undefined || id === "" || id === 0) {
            setLoading(true);
        }
        else {
            axios.get("https://localhost:44352/api/review/" + id,config).then((response) => {
                getData(response.data)
            }).catch((err) => err.name);
        }
    }, [])


    const getData = (data) => {
        setLoading(true)
        setTitle(data.title)
        setAbout(data.about)
        setDescription(data.description)
        setImage(data.image)
        setDate(data.date)
        setCategory(data.category.categoryName)
        setUser(data.user.email)
        setVideoUrl(data.videoUrl)
        setTimeout(function () {
            setLoading(false)
        }, 1000);
    }
    return (

        <div className="container">
            <div className="shadow mt-5">
                <div className="p-3">
                    {Loading ? <div className="row align-content-center">
                        <div className="d-flex justify-content-center p-5">
                            <div className="spinner-border text-info spinner-size" role="status">
                                <span className="visually-hidden">Loading...</span>
                            </div>

                        </div>
                        <div className="d-flex justify-content-center p-5">
                            Please wait
                        </div>
                    </div> :
                        <div class="clearfix">
                            <img src={BaseImageUrl + Image} className="col-md-6 img-fluid float-md-end mb-3 ms-md-3 shadow-lg rounded-3" alt="..." />
                            <h1 className="text-danger">
                                {Title}
                            </h1>
                            <hr />
                            <p className="lh-lg">
                                <h3 className="lh-lg">{About}</h3>
                            </p>
                            <div className="row pb-3">
                                <div className="col-md-10">
                                    <Facebook url={url} />
                                    <Twitter url={url} shareText={shareText} />
                                </div>
                            </div>
                            <h5 className="lh-lg">
                                {Description}
                            </h5>
                            <div className="row pt-5 pb-5">
                                <div className="col-lg-4 col-md-4 col-sm-4">
                                    <i className="bi bi-calendar3 text-info"></i> {moment(Date).format('dddd, MMMM yyyy')}
                                </div>
                                <div className="col-lg-4 col-md-4 col-sm-4">
                                <i className="bi bi-person"></i>{User}
                                </div>
                                <div className="col-lg-4 col-md-4 col-sm-4">
                                <i className="bi bi-tags-fill text-success"></i> {Category}
                                </div>
                            </div>
                        </div>
                    }
                </div>
            </div>
        </div>
    )
}