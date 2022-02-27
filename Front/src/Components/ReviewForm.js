import React, { useState, useEffect } from "react";
import { useAlert } from 'react-alert'
import axios from "axios";
import Cookies from 'universal-cookie';

const ReviewForm = (props) => {
    const cookies = new Cookies();
    const token="Bearer "+ cookies.get('Token');
    const config = { headers: { 'Content-Type': 'multipart/form-data' ,'Authorization': token} };
    const [CategoryAPI, setCategoryAPI] = useState([]);
    const [Title, setTitle] = useState('');
    const [About, setAbout] = useState('');
    const [Description, setDescription] = useState('');
    const [Image, setImage] = useState();
    const [ImageFile, setImageFile] = useState()
    const [CategoryId, setCategoryId] = useState(1);
    const [UserId, setUserId] = useState('');
    const [VideoUrl, setVideoUrl] = useState('');
    const alert = useAlert()

    useEffect(() => {
        axios.get("https://localhost:44352/api/category").then((response) => {
            setCategoryAPI(response.data)
        }).catch((err) => err.name);

    }, [])

    const postData = (e) => {
        e.preventDefault();
        setUserId(props.userId)
        const formData = new FormData();
        formData.append("Title", Title)
        formData.append("About", About)
        formData.append("Description", Description)
        formData.append("Image", Image)
        formData.append("ImageFile", ImageFile)
        formData.append("CategoryId", CategoryId)
        formData.append("UserId", props.userId)

        axios.post("https://localhost:44352/api/Review/", formData, config)
            .then((response) => {
                if (response) {
                    alert.success("Your review posted on Gamehub.")
                }
            }
            ).catch((err) => alert.error("Post failed"))

    }


    const saveFile = (e) => {
        setImage(e.target.files[0].name);
        setImageFile(e.target.files[0]);
    }
    return (
        <div>
            <form onSubmit={postData}>
                <div className="row">
                    <div className="col-lg-6">
                        <div className="form-floating mb-3">
                            <input className="form-control" onChange={(e) => setTitle(e.target.value)} id="title" type="text" placeholder="Title" />
                            <label className="form-control-label" htmlFor="title">Title</label>
                        </div>
                    </div>
                    <div className="col-lg-6">
                        <div className="form-floating mb-3">
                            <input className="form-control" onChange={(e) => setAbout(e.target.value)} id="about" type="text" placeholder="About" />
                            <label className="form-control-label" htmlFor="about">About</label>
                        </div>
                    </div>
                </div>

                <div className="row pt-4 pb-4">
                    <div className="col-md-lg col-md-12">
                        <div className="form-floating">
                            <textarea className="form-control" onChange={(e) => setDescription(e.target.value)} placeholder="Leave a description here" id="description"></textarea>
                            <label htmlFor="description">Description</label>
                        </div>
                    </div>
                    <div className="col-lg-4 col-md-12">
                        <div className="input-group mt-2">
                            <input type="file" onChange={saveFile} className="form-control" id="imageFile" />
                        </div>
                    </div>
                </div>
                <div className="row pt-4 pb-4">
                    <div className="col-md-6">
                        <select onChange={(e) => setCategoryId(e.target.value)} className="form-select form-select-lg mb-3 dropdownheight" aria-label="category">
                            <option defaultValue >Select your category</option>
                            {
                                CategoryAPI.map((data) => {
                                    return (
                                        <option value={data.id} key={data.id}>{data.categoryName}</option>
                                    )
                                })
                            }
                        </select>
                    </div>
                    <div className="col-md-6">
                        <div className="form-floating">
                            <input className="form-control" onChange={(e) => setVideoUrl(e.target.value)} id="videourl" type="text" placeholder="videourl" />
                            <label className="form-control-label" htmlFor="videourl">Video Url</label>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="col-md-12 text-end">
                        <button className="btn btn-primary btn-lg shadow " type="submit">Post review</button>

                    </div>
                </div>

            </form>
        </div>
    )
}
export default ReviewForm;